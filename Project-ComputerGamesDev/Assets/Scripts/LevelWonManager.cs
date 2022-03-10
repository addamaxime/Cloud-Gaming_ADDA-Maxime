using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelWonManager : MonoBehaviour{
    public GameObject levelWonManagerUI;
    public static LevelWonManager instance ;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of GameOverManager");
            return;
        }
        instance = this;
    }
    
    public void OnPlayerDeath(){
        if(CurrentSceneManager.instance.isPlayerPresentByDefault){
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        levelWonManagerUI.SetActive(true);
    }

    public void PlayAgainButton(){
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        /*Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedCount);*/
        PlayerHealth.instance.Respawn();
        levelWonManagerUI.SetActive(false);
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
