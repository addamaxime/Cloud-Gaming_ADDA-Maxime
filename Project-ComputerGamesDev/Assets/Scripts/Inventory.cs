using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour{
    public int totalCoins;
    public Text totalCoinsText;
    public static Inventory instance;

    private void Awake(){
        if(instance !=null){
            Debug.LogWarning("More than one instance of the inventory");
            return;
        }
        instance = this;
    }

    public void addCoins(int count){
        totalCoins += count ;
        totalCoinsText.text = totalCoins.ToString();
    }

    public void RemoveCoins(int count){
        totalCoins -= count ;
        totalCoinsText.text = totalCoins.ToString();
    }


}
