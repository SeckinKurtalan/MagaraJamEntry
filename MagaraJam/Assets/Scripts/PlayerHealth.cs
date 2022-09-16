using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    Rigidbody rb;
    float health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        healthText.text = "Health: " + health.ToString();
    }
    public void UpdateHealth(float damage)
    {
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
