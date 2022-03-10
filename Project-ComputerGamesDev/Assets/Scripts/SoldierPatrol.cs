using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPatrol : MonoBehaviour
{
    public float speed;
    public float timeBetweenShots;
    public float shootSpeed;
    public float range;
    private float distToPlayer;
    public Transform[] waypoints;

    public SpriteRenderer graphics;

    private Transform target;
    public Transform shootPos;
    private Transform player;
    public GameObject bullet;
    private bool shotActivated = false;
    public int maxHealth;
    public int currentHealth;
    public int healthPointsGivenToThePlayer;
    public int attackPointsToThePlayer;

    private int destPoint;

    public Animator animator;

    public static SoldierPatrol instance;
    public AudioClip bulletSound;
    public AudioClip hitSoundEnemyDead;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of SoldierPatrol");
            return;
        }
        instance = this;
    }
    
    void Start()
    {
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
        player = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
        target = waypoints[0];
    }

    
    void Update(){
        distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer <=range && !shotActivated){
            StartCoroutine(BulletShot());
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    public IEnumerator BulletShot(){
        AudioManager.instance.PlayClipAt(bulletSound,transform.position);
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
            Die();
        }
    }

    public void Die(){
        this.GetComponent<Collider2D>().enabled = false;
        PlayerHealth.instance.HealPlayer(healthPointsGivenToThePlayer);
        AudioManager.instance.PlayClipAt(hitSoundEnemyDead,transform.position);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Shuriken")){
            TakeDamage(PlayerMovement.instance.damage);
        }

        if(collision.transform.CompareTag("Player")){
            PlayerHealth.instance.TakeDamage(SoldierPatrol.instance.attackPointsToThePlayer);
            TakeDamage((PlayerMovement.instance.damage)%2);
        }
    }  
}
