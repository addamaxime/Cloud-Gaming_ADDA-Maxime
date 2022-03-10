using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameOverManager instance ;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of GameOverManager");
            return;
        }
        instance = this;
    }
    
    public void OnPlayerDeath(){
        Timer.instance.EndTimer();
        if(CurrentSceneManager.instance.isPlayerPresentByDefault){
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        gameOverUI.SetActive(true);
    }

    public void PlayAgainButton(){
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        /*Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedCount);*/
        PlayerHealth.instance.Respawn();
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("Level01");
    }
    public void HomeButton(){
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu");
    }

    public void LeaveButton(){
        Application.Quit();        
    }
}
