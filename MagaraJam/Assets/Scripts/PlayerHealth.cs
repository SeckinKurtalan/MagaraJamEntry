using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    //[SerializeField] TextMeshProUGUI healthText;
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
        //healthText.text = "Health: " + health.ToString();
    }
    public void UpdateHealth(float damage)
    {
        soundSc.HurtSound();
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        //healthText.text = "Health: " + health.ToString();
        healthBar.SetHealth(health);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
