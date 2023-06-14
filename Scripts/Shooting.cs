using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public float damage;
    public float Range;
    public Transform Camera;
    public LayerMask EnemyLayer;
    public float ShootBetweenTime;
    public float StartTime;
    public ParticleSystem GunPoooof;
    private InputManager inputManager;
    public Animator GunAnim;
    private RaycastHit Hit;
    public float PushBackEnemy;
    public GameObject GunParticle;
    public GameObject BloodParticle;
    public GameObject FloorParticle;

    void Start(){
        inputManager = InputManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        shooting();
    }

    void shooting(){
        if(inputManager.GetShoot() > 0){

            if(ShootBetweenTime <= 0)
            {
                ShootMain();
                ShootBetweenTime = StartTime;
                GunAnim.SetTrigger("Shoot");
                print("sdadadad");
            }
        }

        else if(ShootBetweenTime > 0)
        {
            ShootBetweenTime -= Time.deltaTime;
        }
    }

    public void ShootMain()
    {
        Physics.Raycast(Camera.position, Camera.forward, out Hit, Range);

        if(Hit.transform.gameObject.GetComponent<Enemy>())
        {
            Hit.transform.gameObject.GetComponent<Enemy>().Damage(damage);
            Instantiate(BloodParticle, Hit.point, Quaternion.LookRotation(Hit.normal));

            if(Hit.rigidbody != null){
                Hit.rigidbody.AddForce(-Hit.normal * PushBackEnemy);
            }
        }
        else{
            Instantiate(FloorParticle, Hit.point, Quaternion.LookRotation(Hit.normal));
        }
    }
}
