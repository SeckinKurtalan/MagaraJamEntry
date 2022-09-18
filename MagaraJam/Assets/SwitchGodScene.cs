using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchGodScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadTheBossFight());
    }

    IEnumerator LoadTheBossFight()
    {
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene("GodFight");
    }
}
