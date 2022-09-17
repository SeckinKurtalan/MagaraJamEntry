using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SettingMenuScripts : MonoBehaviour
{

    [SerializeField] AudioMixer mainMixer;
    
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }

}
