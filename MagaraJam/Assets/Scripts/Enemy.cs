using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float health;
    [SerializeField] AudioClip hurtSound;
    public float enemiesSpeed;
    public float firstSpeed;
    bool isDead;
    AudioSource audioSource;
    private void Start()
    {
        firstSpeed = enemiesSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        audioSource.PlayOneShot(hurtSound);
        health -= damage;
        if (health <= 0 && !isDead)
        {
            KillEnemy();
        }
        enemiesSpeed = enemiesSpeed * -1.5f;
        StartCoroutine(ResetSpeed());
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
        isDead = true;
        Destroy(gameObject);
    }

}
