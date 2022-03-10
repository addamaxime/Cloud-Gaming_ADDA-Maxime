using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public SpriteRenderer graphics; 

    private Transform target;

    private int destPoint=0;
    public float timeBetweenShots;
    public int attackPointsToThePlayer;

    public GameObject bullet;
    private bool shotActivated = false;
    public Transform shootPos;
    public static PlanePatrol instance;
    public int maxHealth;
    public AudioClip hitSoundEnemyDead;
    int currentHealth;
    public Animator animator;
    public int healthPointsGivenToThePlayer;

    private void Awake(){
        /*if (instance !=null){
            Debug.LogWarning("More than one instance of PlanePatrol");
            return;
        }*/
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
                maxHealth = 20;
                timeBetweenShots = 3f; 
                break;

            case "Medium":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 50;
                timeBetweenShots = 2f; 
                maxHealth = 50;
                break;
                
            case "Hard":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 0;
                maxHealth = 200;
                timeBetweenShots = 1f; 
                break;
        }
        currentHealth = maxHealth;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);  
      
        if(Vector3.Distance(transform.position, target.position)<0.3f){
            destPoint = (destPoint+1)% waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }

        if(!shotActivated){
            StartCoroutine(BulletShot());
        }
    }

    public IEnumerator BulletShot(){
        Instantiate(bullet,shootPos.position,shootPos.rotation);
        shotActivated = true;
        yield return new WaitForSeconds(timeBetweenShots);
        shotActivated =false;
    }

    public void TakeDamage(int damage){
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth<=0){
            animator.SetBool("IsDead",true);
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die(){
        this.GetComponent<Collider2D>().enabled = false;
        PlayerHealth.instance.HealPlayer(healthPointsGivenToThePlayer);
        AudioManager.instance.PlayClipAt(hitSoundEnemyDead,transform.position);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
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
