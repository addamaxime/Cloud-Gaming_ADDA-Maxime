using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject: MonoBehaviour
{
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            AudioManager.instance.PlayClipAt(sound, transform.position);
            Inventory.instance.addCoins(1);
            CurrentSceneManager.instance.coinsPickedCount++;
            Destroy(gameObject);
        }
    }
}
