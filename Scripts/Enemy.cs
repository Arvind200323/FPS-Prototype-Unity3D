using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    public float Speed;
    public Rigidbody rigidbody;
    private GameObject player;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        y = transform.position.y;
        FindObjectOfType<EnemySpawner>().AddEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        Vector3 MOVEPOSITION = (transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed));
        rigidbody.MovePosition(MOVEPOSITION);
    }
    

    public void Damage(float semmaAdi){
        Health -= semmaAdi;

        if(Health <= 0){
            FindObjectOfType<EnemySpawner>().RemoveEnemy();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision player){
        if(player.gameObject.CompareTag("Player")){
            FindObjectOfType<PlayerMovement>().GetDamage();
        }
    }
}
