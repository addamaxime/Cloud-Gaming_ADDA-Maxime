using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool timerHasBegin=false;
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    public int damage;

    // To know if the player is in the air and touching the ground
    public bool isJumping;
    public bool isGrounded;
    
    [HideInInspector]
    public bool isClimbing;

    public AudioClip shurikenSound;
    public AudioClip jumpSound;
    public GameObject bulletToFire ;
    public Transform firePoint;

    public float timeBetweenShurikens;
    // To check Left & Righy
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D playerCollider;
    private Vector3 velocity = Vector3.zero;
    public float horizontalMovement;

    private float verticalMovement;

    private bool shotActivated = false;

    public static PlayerMovement instance;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of PlayerMovement");
            return;
        }
        instance = this;
    }

    void Start(){
        GameObject theDifficulty = GameObject.Find("Difficulty");
        DontDestroyDifficulty difficultyOnScript = theDifficulty.GetComponent<DontDestroyDifficulty>();

        switch(difficultyOnScript.difficulty){
            case "Easy":
                timeBetweenShurikens = 0.3f;
                moveSpeed = 300f;
                damage = 100;
                break;

            case "Medium":
                timeBetweenShurikens = 2f;
                moveSpeed = 250f;
                damage = 50;
                break;
                
            case "Hard":
                timeBetweenShurikens = 3f;
                moveSpeed = 250f;
                damage = 30;
                break;
        }
    }

    void Update(){

        if (!timerHasBegin){
            Timer.instance.BeginTimer();
            timerHasBegin =true;
        }

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded){
            AudioManager.instance.PlayClipAt(jumpSound,transform.position);
            isJumping = true;
        }
        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);

        if ((Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.RightControl)) && (!shotActivated) ){
            StartCoroutine(ShurikenShot());
        }
    }
    void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        MovePlayer(horizontalMovement,verticalMovement); 
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement){
        if(!isClimbing){
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping){
                rb.AddForce(new Vector2(0f,jumpForce));
                isJumping = false;
            }
        }else{
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity){
        if (_velocity > 0.1f){
            spriteRenderer.flipX=false;
        }else if(_velocity<-0.1f){
            spriteRenderer.flipX=true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public IEnumerator ShurikenShot(){  
        Instantiate(bulletToFire, firePoint.position,firePoint.rotation);
        AudioManager.instance.PlayClipAt(shurikenSound,transform.position);
        shotActivated = true;
        yield return new WaitForSeconds(timeBetweenShurikens);
        shotActivated =false;
    }
}
