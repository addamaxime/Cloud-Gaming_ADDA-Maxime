using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    
    public Image fill;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        

        // The maximum of the color gradient
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        // transform the value between 1 and 100 into value for the color gradient
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    
}