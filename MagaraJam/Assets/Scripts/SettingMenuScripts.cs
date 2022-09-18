using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingMenuScripts : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject changeLevel;
    [SerializeField] Slider slider;

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume()
    {
        AudioListener.volume = slider.value;
    }

    public void StartTheGame()
    {
        StartCoroutine(StartTheGameAfterDelay());
    }
    IEnumerator StartTheGameAfterDelay()
    {
        changeLevel.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(5);

    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void ButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
