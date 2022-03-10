using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public int healthPoints;
    public AudioClip hitSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it's a collision with the Player
        if (collision.CompareTag("Player"))
        {
            // We destroy the object (enemy)
            PlayerHealth.instance.HealPlayer(healthPoints);
            AudioManager.instance.PlayClipAt(hitSound,transform.position);
            Destroy(objectToDestroy);
        }
 
    }
}
