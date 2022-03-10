using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyDifficulty : MonoBehaviour
{
    public string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        difficulty = "Easy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setDifficulty(string difficultyReceived){
        difficulty = difficultyReceived;
    }
}
