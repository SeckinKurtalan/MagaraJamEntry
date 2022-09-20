using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Sprite soundOn, soundOff;
    [SerializeField] Image soundButtonImg;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] PlayerControllerPatched playerSc;
    [SerializeField] GameObject changePanel;
    bool isPause;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pausee();
            }
        }
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        playerSc.enabled = true;
        isPause = false;
    }
    public void Pausee()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        playerSc.enabled = false;
        isPause = true;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(RestartAfterDelay());

    }
    IEnumerator RestartAfterDelay()
    {
        changePanel.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        changePanel.SetActive(true);
        Time.timeScale = 1;
        StartCoroutine(MainMenuAfterDelay());

    }
    IEnumerator MainMenuAfterDelay()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(0);
    }
    public void ChangeSound()
    {
        if (AudioListener.volume != 0)
        {
            AudioListener.volume = 0;
            soundButtonImg.sprite = soundOff;
        }
        else
        {
            AudioListener.volume = 1;
            soundButtonImg.sprite = soundOn;
        }
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void ButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}

