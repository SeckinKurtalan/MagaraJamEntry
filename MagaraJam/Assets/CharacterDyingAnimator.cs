using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDyingAnimator : MonoBehaviour
{

    [SerializeField] GameObject enemy;    
    
    
    public void KillEnemy()
    {
        enemy.SetActive(false);
    }
}
