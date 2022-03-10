using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{

    public string sceneName;
    public Animator fadeSystem;
    public AudioClip nextLevelSong;

    private void Awake(){
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene(){
        AudioManager.instance.PlayClipAt(nextLevelSong,transform.position);
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        AudioManager.instance.PlayNextSong();
        
        if(sceneName == "EndScene"){
            LevelWonManager.instance.levelWonManagerUI.SetActive(true);
            Timer.instance.EndTimer();
            foreach(var element in DontDestroyOnLoadScene.instance.objects){
                if(element.name == "Player"){
                    element.SetActive(false);
                }
            }
        }
    }
}
