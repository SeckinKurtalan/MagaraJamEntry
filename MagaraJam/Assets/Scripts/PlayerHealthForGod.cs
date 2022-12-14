using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealthForGod : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject God;
    [SerializeField] GameObject player;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] float maxHealth;
    Rigidbody rb;
    public HealthBar healthBar;
    PlayerSound soundSc;
    public float health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundSc = GetComponent<PlayerSound>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void UpdateHealth(float damage)
    {
        soundSc.HurtSound();
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
        Physics.IgnoreLayerCollision(6, 3);
        StartCoroutine(ResetCollision());
    }
    IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(1f);
        Physics.IgnoreLayerCollision(6, 3, false);
    }
    public void Die()
    {
        SceneManager.LoadScene("TheEndScene");
    }
    
    

}
