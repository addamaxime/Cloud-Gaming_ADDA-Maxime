using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartialEnemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth;
    int currentHealth;
    public int healthPointsGivenToThePlayer;
    public int attackPointsToThePlayer;

    public AudioClip hitSoundEnemyDead;
    public AudioClip hitSoundTouched;


    void Start()
    {
        GameObject theDifficulty = GameObject.Find("Difficulty");
        DontDestroyDifficulty difficultyOnScript = theDifficulty.GetComponent<DontDestroyDifficulty>();

        switch(difficultyOnScript.difficulty){
            case "Easy":
                attackPointsToThePlayer = 10;
                healthPointsGivenToThePlayer = 100;
                maxHealth = 20;
                break;

            case "Medium":
                attackPointsToThePlayer = 50;
                healthPointsGivenToThePlayer = 50;
                maxHealth = 50;
                break;
                
            case "Hard":
                attackPointsToThePlayer = 100;
                healthPointsGivenToThePlayer = 0;
                maxHealth = 100;
                break;
        }
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth<=0){
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die(){
        animator.SetBool("IsDead",true);
        this.GetComponent<Collider2D>().enabled = false;
        PlayerHealth.instance.HealPlayer(healthPointsGivenToThePlayer);
        AudioManager.instance.PlayClipAt(hitSoundEnemyDead,transform.position);
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Player")){
            PlayerHealth.instance.TakeDamage(attackPointsToThePlayer);
        }
        if(collision.transform.CompareTag("Shuriken")){
            TakeDamage(PlayerMovement.instance.damage);
        }
    }    
}
