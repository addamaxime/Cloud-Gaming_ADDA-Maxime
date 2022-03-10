using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = -transform.right*speed;
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Player"){
            PlayerHealth.instance.TakeDamage(StaticSoldier.instance.attackPointsToThePlayer);
            Destroy(gameObject);
        }
        if(other.gameObject.name == "BulletDestroyCollider"){
            Destroy(gameObject);
        }          
    } 
}
