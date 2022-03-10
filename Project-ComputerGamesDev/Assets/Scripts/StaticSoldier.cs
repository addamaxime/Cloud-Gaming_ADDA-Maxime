using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSoldier : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firepoint;

    public float timeBetweenBullets;
    private bool shotActivated = false;

    public int maxHealth;
    int currentHealth;
    public int healthPointsGivenToThePlayer;
    public int attackPointsToThePlayer;

    public AudioClip hitSoundEnemyDead;
    public AudioClip hitSoundTouched;

    public static StaticSoldier instance;
    public AudioClip bulletSound;
    public Animator animator;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of StaticSoldier");
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
                timeBetweenBullets = 3f; 
                break;

            case "Medium":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 50;
                timeBetweenBullets = 2f; 
                maxHealth = 50;
                break;
                
            case "Hard":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 0;
                maxHealth = 200;
                timeBetweenBullets = 1f; 
                break;
        }
        currentHealth = maxHealth;
    }

    void Update(){
        if(!shotActivated){
        StartCoroutine(BulletShot());
        }
    }

    public IEnumerator BulletShot(){
        AudioManager.instance.PlayClipAt(bulletSound,transform.position);
        Instantiate(bulletToFire,firepoint.position,firepoint.rotation);
        shotActivated = true;
        yield return new WaitForSeconds(timeBetweenBullets);
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
    }    
}
