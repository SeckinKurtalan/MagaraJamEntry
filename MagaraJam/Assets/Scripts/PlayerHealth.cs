using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    Rigidbody rb;
    PlayerSound soundSc;
    float health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundSc = GetComponent<PlayerSound>();
        health = maxHealth;
        healthText.text = "Health: " + health.ToString();
    }
    public void UpdateHealth(float damage)
    {
        soundSc.HurtSound();
        health -= damage;
        healthText.text = "Health: " + health.ToString();
        Physics.IgnoreLayerCollision(6, 3);
        StartCoroutine(ResetCollision());
    }
    IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(1f);
        Physics.IgnoreLayerCollision(6, 3, false);
    }

}
