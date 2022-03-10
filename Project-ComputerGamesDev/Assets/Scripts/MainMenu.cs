using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsWindow;
    public GameObject howToPlayWindow;
    public string levelToLoad;
    public void StartGameButton(){
        SceneManager.LoadScene(levelToLoad);
    }
    public void SettingsButton(){
        settingsWindow.SetActive(true);                
    }

    public void HowToPlayButton(){
        howToPlayWindow.SetActive(true);                
    }

    public void LoadCreditsSceneButton(){
        SceneManager.LoadScene("Credits");                
    }

    public void CloseSettings(){
        settingsWindow.SetActive(false);
    }
    public void CloseHowToPlay(){
        howToPlayWindow.SetActive(false);
    }
    public void QuitGameButton(){
        Application.Quit();
    }
}
