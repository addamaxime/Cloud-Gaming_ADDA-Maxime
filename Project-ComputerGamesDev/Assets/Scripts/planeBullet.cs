using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 4f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Player"){
            PlayerHealth.instance.TakeDamage(PlanePatrol.instance.attackPointsToThePlayer);
            Destroy(gameObject);
        }
        if(other.gameObject.name != "Player" && other.gameObject.name != "PlaneSprite"){
            Destroy(gameObject);
        }          
    } 
}
