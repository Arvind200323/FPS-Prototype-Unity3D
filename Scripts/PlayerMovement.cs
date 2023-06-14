using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject Camera;
    public Vector3 playerVelocity;
    public float playerSpeed = 2.0f;
    private float StartPlayerSped;
    public float jumpHeight = 1.0f;
    public float xRotationSensitivity = 1;
    public float yRotationSensitivity = 1;
    public float gravityValue = -9.81f;
    public float StartGravityValue;
    private InputManager inputManager;
    private float RotationX;
    private float RotationY;
    public bool IsGrounded;
    public Transform IsgroundedPos;
    public float IsgroundedRadius;
    public LayerMask IsgroundedMask;

    public float CrouchValue;
    public float CrouchSlowDown;
    public float SprintSpeed;
    private float CurrentSprintSpeed;
    private bool Stop;

    public float WallRunRadius;
    public bool CanWallRun;
    public Transform LeftDetect;
    public Transform RightDetect;
    public LayerMask WallRunMask;
    public Animator PlayerAnim;

    public GameObject Gun;

    public float Health;
    public GameObject RetryPanel;
    private bool StopMove = false;

    
    private void Start(){
        inputManager = InputManager.Instance;
        StartPlayerSped = playerSpeed;
        StartGravityValue = gravityValue;
        Cursor.lockState = CursorLockMode.Locked;
        RetryPanel.SetActive(false);
    }

    void Update()
    {  
        IsGrounded = Physics.CheckSphere(IsgroundedPos.position, IsgroundedRadius, IsgroundedMask);
        
        if(!StopMove)
        {
        Move();
        Jump();
        RotateCam();
        Crouch();
        Sprint();
        WallRun();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Move(){
        Vector2 Movement = inputManager.GetMovement();
        Vector3 move = transform.right * Movement.x + transform.forward * Movement.y;
        
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    void Jump(){
        if (IsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (inputManager.GetJump() && IsGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    void RotateCam(){
        Vector2 CamLook = inputManager.GetLook();
        RotationX -= CamLook.y;
        float ClampRotX = Mathf.Clamp(RotationX, -200, 200);

        gameObject.transform.Rotate(Vector3.up * CamLook.x * xRotationSensitivity);
        Camera.transform.localRotation = Quaternion.Euler(ClampRotX * yRotationSensitivity, 0, 0); 
    }

    void Crouch(){
        if(inputManager.GetCrouch() > 0){
            transform.localScale = new Vector3(1, CrouchValue, 1);
            Gun.transform.localScale = new Vector3(1, 2.6f, 1);
            
            if(!Stop){
                playerSpeed = playerSpeed * 1.25f;
                Stop = true;
            }

            if(playerSpeed <= 0){
                return;
            }  
            else{
                playerSpeed -= Time.deltaTime * CrouchSlowDown; 
            }
        }
        else if(inputManager.GetCrouch() == 0){
            transform.localScale = new Vector3(1, 1, 1);
            Gun.transform.localScale = new Vector3(1, 1, 1);
            playerSpeed = StartPlayerSped;
            Stop = false;
        }
    }

    void Sprint(){
        if(inputManager.GetSprint() > 0){
        Vector2 Movement = inputManager.GetMovement();
        Vector3 move = transform.right * Movement.x + transform.forward * Movement.y;
        
        CurrentSprintSpeed = Mathf.Lerp(playerSpeed, SprintSpeed, 0.2f);

        controller.Move(move * Time.deltaTime * CurrentSprintSpeed);
        }
        else{
            CurrentSprintSpeed = StartPlayerSped;
        }
    }

    void WallRun(){
        if(Physics.CheckSphere(LeftDetect.position, WallRunRadius, WallRunMask) || Physics.CheckSphere(RightDetect.position, WallRunRadius, WallRunMask)){
            CanWallRun = true;
            gravityValue = -10;
            WallRunSub();
        }

        else if(!Physics.CheckSphere(LeftDetect.position, WallRunRadius, WallRunMask) || !Physics.CheckSphere(RightDetect.position, WallRunRadius, WallRunMask)){
            CanWallRun = false;
            gravityValue = StartGravityValue;
            PlayerAnim.SetBool("Left Wall", false);
            PlayerAnim.SetBool("Right Wall", false);
        }
        if(IsGrounded == true){
            CanWallRun = false;
            gravityValue = StartGravityValue;
            PlayerAnim.SetBool("Left Wall", false);
            PlayerAnim.SetBool("Right Wall", false);
        }
    }

    void WallRunSub(){
        if(IsGrounded == false){
            if(Physics.CheckSphere(LeftDetect.position, WallRunRadius, WallRunMask)){
                PlayerAnim.SetBool("Left Wall", true);
            }
            else{
                PlayerAnim.SetBool("Right Wall", true);
            }
        }
    }

    public void GetDamage(){
        Health -= 10;

        if(Health <= 0){
            RetryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            StopMove = true;
        }    
    }
}
