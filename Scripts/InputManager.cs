using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance{
        get{
                return _instance;
            }
    }

    private PlayerInput PlayerInput;

    public void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            _instance = this;
        }

        PlayerInput = new PlayerInput();
    }

    private void OnEnable(){
        PlayerInput.Enable();
    }

    private void OnDisable(){
        PlayerInput.Disable();
    }

    public Vector2 GetMovement(){
        return PlayerInput.PlayerControls.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetLook(){
        return PlayerInput.PlayerControls.Look.ReadValue<Vector2>();
    }

    public bool GetJump(){
        return PlayerInput.PlayerControls.Jump.triggered;
    }

    public float GetCrouch(){
        return PlayerInput.PlayerControls.Crouch.ReadValue<float>();
    }

    public float GetSprint(){
        return PlayerInput.PlayerControls.Sprint.ReadValue<float>();
    }

    public float GetShoot(){
        return PlayerInput.PlayerControls.Shoot.ReadValue<float>();
    }
}
