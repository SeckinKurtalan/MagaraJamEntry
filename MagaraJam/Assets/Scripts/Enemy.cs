using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{



    [SerializeField] Animator enemyAnim;

    public float health;
    [SerializeField] AudioClip hurtSound;
    public float enemiesSpeed;
    public float firstSpeed;
    public bool isDead;
    AudioSource audioSource;


    private void Start()
    {
        firstSpeed = enemiesSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            if (health <= 0)
            {
                KillEnemy();
            }
            audioSource.PlayOneShot(hurtSound);
            enemiesSpeed = enemiesSpeed * -3f;
            StartCoroutine(ResetSpeed());
        }
    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(.25f);
        enemiesSpeed = 0;
        yield return new WaitForSeconds(.25f);
        enemiesSpeed = firstSpeed;

    }

    private void KillEnemy()
    {
        enemyAnim.SetTrigger("Death");
        isDead = true;
    }

}
