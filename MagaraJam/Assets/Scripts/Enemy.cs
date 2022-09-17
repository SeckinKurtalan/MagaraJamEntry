using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{


    [SerializeField] AudioClip deathSound;
    [SerializeField] Animator enemyAnim;
    public float health;
    [SerializeField] AudioClip[] hurtSound;
    public float enemiesSpeed;
    public float firstSpeed;
    public bool isDead;
    AudioSource audioSource;
    public bool isHurt;
    private int i = 0;


    private void Start()
    {
        firstSpeed = enemiesSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {

        if (!isDead)
        {
            isHurt = true;
            health -= damage;
            if (health <= 0)
            {
                KillEnemy();
            }
            i++;
            if (i == 2) { i = 0;}
            audioSource.PlayOneShot(hurtSound[i]);
            enemiesSpeed = enemiesSpeed * -2f;
            StartCoroutine(ResetSpeed());
        }
    }
    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(.25f);
        enemiesSpeed = 0;
        yield return new WaitForSeconds(.25f);
        enemiesSpeed = firstSpeed;
        isHurt = false;

    }

    private void KillEnemy()
    {
        audioSource.PlayOneShot(deathSound);
        enemyAnim.SetTrigger("Death");
        isDead = true;
    }

}
