 using UnityEngine;
  using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMovement playerMovement;

    public BoxCollider2D topCollider;
    public Text interactUI;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if(isInRange && playerMovement.isClimbing &&(Input.GetKeyDown(KeyCode.LeftAlt) || (Input.GetKeyDown(KeyCode.RightAlt)))){
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            return;
        }
        if (isInRange && (Input.GetKeyDown(KeyCode.LeftAlt) || (Input.GetKeyDown(KeyCode.RightAlt)))){
            playerMovement.isClimbing = true;
            topCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        isInRange = false;
        playerMovement.isClimbing = false;
        topCollider.isTrigger = false;
        interactUI.enabled = false;
    }
}
