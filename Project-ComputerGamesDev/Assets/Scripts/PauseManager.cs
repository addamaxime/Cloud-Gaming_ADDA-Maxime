using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject settingsWindow;
    public GameObject howToPlayWindow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }else{
                if(GameOverManager.instance.gameOverUI.activeSelf || LevelWonManager.instance.levelWonManagerUI.activeSelf){
                }else{
                Paused();
                }
            }
        }
    }
    void Paused(){
        PlayerMovement.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }
    public void Resume(){
        PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void OpenSettingsWindow(){
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow(){
        settingsWindow.SetActive(false);
    }

    public void OpenHowToPlayWindow(){
        howToPlayWindow.SetActive(true);
    }

    public void CloseHowToPlayWindow(){
        howToPlayWindow.SetActive(false);
    }

    public void LoadMainMenu(){
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");

    }
}
