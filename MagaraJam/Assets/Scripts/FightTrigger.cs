using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    [SerializeField] GameObject fightArea;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] GameObject[] roads;
    [SerializeField] GameObject enemieTag;
    [SerializeField] GameObject fightText;
    bool isInFightArea;
    bool isOutFightArea;
    string enemieTagString;

    private void Start()
    {
        enemieTagString = enemieTag.tag;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isInFightArea)
        {
            fightArea.SetActive(true);
            fightText.SetActive(true);
            backgroundMusic.Stop();
            isInFightArea = true;
            foreach (GameObject road in roads)
            {
                road.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (isInFightArea)
        {
            if (GameObject.FindGameObjectsWithTag(enemieTagString).Length <= 0)
            {
                if (!isOutFightArea)
                {
                    isOutFightArea = true;
                    fightArea.SetActive(false);
                    backgroundMusic.Play();
                    fightText.SetActive(false);
                    foreach (GameObject road in roads)
                    {
                        road.SetActive(true);

                    }
                }
            }
        }
    }
}
