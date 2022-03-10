using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    public float speed ;
    public Rigidbody2D therigidbody2D;

    void Start(){

    }

    void Update(){
        
        therigidbody2D.velocity = transform.right*speed;
        transform.Rotate(5,0,0);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name != "Player"){
        Destroy(gameObject);
        }        
    } 

   private void onBecameInvisible(){
        Destroy(gameObject);
    }
}
