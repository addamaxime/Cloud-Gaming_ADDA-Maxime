using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth ;
    public int currentHealth;

    public bool isInvicible = false;
    public AudioClip hitSound;

    public float invincibilityTimeDelay = 0.2f ;
    public float invincibilityDuration;
    public SpriteRenderer graphics;

    public HealthBar healthBar;

    public static PlayerHealth instance;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of Player");
            return;
        }
        instance = this;
    }

    void Start(){
        GameObject theDifficulty = GameObject.Find("Difficulty");
        DontDestroyDifficulty difficultyOnScript = theDifficulty.GetComponent<DontDestroyDifficulty>();

        switch(difficultyOnScript.difficulty){
            case "Easy":
                maxHealth = 300;
                invincibilityDuration = 3f;
                break;

            case "Medium":
                maxHealth = 100;
                invincibilityDuration = 2f;
                break;
                
            case "Hard":
                maxHealth = 1;
                invincibilityDuration = 1f;
                break;
        }
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.H)){
            TakeDamage(60);
        }
    }

    public void HealPlayer(int amount){
        if((currentHealth+amount)>maxHealth){
            currentHealth = maxHealth;
        }else{
        currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
    }
        public void TakeDamage(int damage){
        if (!isInvicible){
        AudioManager.instance.PlayClipAt(hitSound,transform.position);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // verify if player is dead or not
        if (currentHealth<=0){
            Die();
            return;
        }
        isInvicible = true;
        StartCoroutine(invincibilityView());
        StartCoroutine(invinibilityStopDelay());
        }
    }

    public void Die(){
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.animator.SetTrigger("Die");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled=false; 
        GameOverManager.instance.OnPlayerDeath();
    }
    
    public void Respawn(){
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled=true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator invincibilityView(){
        while(isInvicible){
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityTimeDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityTimeDelay);
        }
    }

    public IEnumerator invinibilityStopDelay(){
        yield return new WaitForSeconds(invincibilityDuration);
        isInvicible = false;
    }
    
}
