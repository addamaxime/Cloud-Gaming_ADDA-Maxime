using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] waypoints;

    public SpriteRenderer graphics; 

    private Transform target;

    private int destPoint=0;
    public float timeBetweenAttacks;
    public int attackPointsToThePlayer;

    public GameObject bullet;
    public static BossScript instance;
    public int maxHealth;
    public AudioClip hitSoundEnemyDead;
    public int currentHealth;

    public bool isAttacking = false;
    
    public Animator animator;
    public int healthPointsGivenToThePlayer;



    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of BossScript");
            return;
        }
        instance = this;
    }
    void Start()
    {        
        target = waypoints[0];      
        GameObject theDifficulty = GameObject.Find("Difficulty");
        DontDestroyDifficulty difficultyOnScript = theDifficulty.GetComponent<DontDestroyDifficulty>();

        switch(difficultyOnScript.difficulty){
            case "Easy":
                attackPointsToThePlayer = 60;
                healthPointsGivenToThePlayer = 100;
                maxHealth = 300;
                break;

            case "Medium":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 50;
                maxHealth = 500;
                break;
                
            case "Hard":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 0;
                maxHealth = 1000;
                break;
        }
        currentHealth = maxHealth;
    }

    void Update(){
        
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); 
      
        if (Vector3.Distance(transform.position, target.position) < 0.3f){
            destPoint = (destPoint + 1) % waypoints.Length;
            // next point to go
            target = waypoints[destPoint];
            //graphics.flipX = !graphics.flipX;
            transform.RotateAround (transform.position, transform.up, 180f);

        }
        if(!isAttacking){
            StartCoroutine(Attack());
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth<=0){
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die(){
        this.GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Die");
        PlayerHealth.instance.HealPlayer(healthPointsGivenToThePlayer);
        AudioManager.instance.PlayClipAt(hitSoundEnemyDead,transform.position);
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }

    public IEnumerator Attack(){         
        animator.SetBool("Attack",true);
        isAttacking = true;  
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
        animator.SetBool("Attack",isAttacking);        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Shuriken")){
            TakeDamage(PlayerMovement.instance.damage);
        }
        if(collision.transform.CompareTag("Player")){
            PlayerHealth.instance.TakeDamage(attackPointsToThePlayer);
        }
        
    }  
}
