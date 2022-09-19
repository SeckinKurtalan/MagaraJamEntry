using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class end : MonoBehaviour
{
    [SerializeField] VideoPlayer video;
    [SerializeField] GameObject changeScene;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        StartCoroutine(ChangeScene());



    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(29f);
        video.Stop();
        changeScene.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
