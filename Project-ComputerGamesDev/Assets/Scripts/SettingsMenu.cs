using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    public Dropdown resolutionsDropDown;
    Resolution[] resolutions;

    public Slider musicSlider;
    public Slider effectsSlider;
    public void Start(){
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider; 

        audioMixer.GetFloat("Effects", out float effectsValueForSlider);
        effectsSlider.value = effectsValueForSlider; 

        resolutions = Screen.resolutions.Select(resolution => new Resolution{width = resolution.width, height = resolution.height}).Distinct().ToArray();
        resolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i =0 ; i<resolutions.Length;i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResolutionIndex = i;
            }
        }
        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = currentResolutionIndex;
        resolutionsDropDown.RefreshShownValue();

        Screen.fullScreen = true;
    }
    public void SetVolume(float volume){
        audioMixer.SetFloat("Music", volume);
    }
    public void SetEffectsVolume(float volume){
        audioMixer.SetFloat("Effects", volume);
    }

    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
