using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false ;
    public int coinsPickedCount;

    public static CurrentSceneManager instance ;

    private void Awake(){
        if (instance !=null){
            Debug.LogWarning("More than one instance of CurrentSceneManager");
            return;
        }
        instance = this;
    }
}
