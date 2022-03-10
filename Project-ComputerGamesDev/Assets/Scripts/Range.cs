using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Player")){
            PlayerHealth.instance.TakeDamage(this.transform.parent.GetComponent<MartialEnemy>().attackPointsToThePlayer);
        }
    }
}
