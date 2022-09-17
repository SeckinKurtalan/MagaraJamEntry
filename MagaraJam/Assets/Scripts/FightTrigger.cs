using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    [SerializeField] GameObject fightArea;
    [SerializeField] AudioSource backgroundMusic;
    bool isInFightArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isInFightArea)
        {
            fightArea.SetActive(true);
            backgroundMusic.Pause();
            isInFightArea = true;
        }
    }
}
