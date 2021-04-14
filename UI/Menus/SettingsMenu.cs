using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
   // references to certain UI elements
    public AudioMixer audioMixer;
    public Dropdown graphicsDropdown;
    public Dropdown resolutionDropDown;

    //an array holding Resoultions
    Resolution[] resolutions;

    //on start, set the Resolution dropdown to accomadate all resolution values. Defaults to the screen's native resolution
    private void Start() {
        graphicsDropdown.value = 2;

        resolutions = Screen.resolutions;   
        resolutionDropDown.ClearOptions(); 

        List<string> resolutionOptions = new List<string>(); 
        
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i ++) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height) {
                    currentResIndex = i;
                }
        }
        resolutionDropDown.AddOptions(resolutionOptions);
        resolutionDropDown.value = currentResIndex;
        resolutionDropDown.RefreshShownValue();
    }


    // below are methods called by individual UI elements set in the Inspector

    //passes the value of the VolumeSlider to the Master AudioMixer
    public void SetGameVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    } 


    /*Takes in the quality index from the dropdown, and sets the quality of the game (qualities are defined in Build Settings) in accordance to 
      dropdown index */
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //if the toggle is on, enable FullScreen
    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }


    //sets the value of the resolution to be what is selected in the dropdown
    public void SetResolution(int resIndex) {
        Resolution resoultion = resolutions[resIndex];
        Screen.SetResolution(resoultion.width, resoultion.height, Screen.fullScreen);
    }
}
