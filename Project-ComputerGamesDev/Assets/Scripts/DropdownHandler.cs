using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    private string TextBox;
    
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        List<string> items = new List<string>();
        items.Add("Easy");
        items.Add("Medium");
        items.Add("Hard");

        foreach(var item in items){
            dropdown.options.Add(new Dropdown.OptionData(){ text = item});
        }
        dropdown.value = 0;
        dropdown.RefreshShownValue();
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSeleted(dropdown);});
    }

    public void DropdownItemSeleted(Dropdown dropdown){
        int index = dropdown.value;
        TextBox = dropdown.options[index].text;
        GameObject theDifficulty = GameObject.Find("Difficulty");
        DontDestroyDifficulty difficultyOnScript = theDifficulty.GetComponent<DontDestroyDifficulty>();
        difficultyOnScript.setDifficulty(TextBox);                
    }
}
