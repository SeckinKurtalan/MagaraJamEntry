using System.Collections.Generic;
using UnityEngine;

public class AngelController : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] GameObject playerHead;
    [SerializeField] AudioClip attackSound;
    AudioSource audioSource;
    Enemy enemy;
    Rigidbody rb;
    bool isRange;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = playerHead.transform.position - transform.position;
        Vector3 directionNorm = direction.normalized;

        transform.LookAt(playerHead.transform.position);

        if (direction.magnitude < attackRange)
        {
            if (!isRange)
            {
                audioSource.PlayOneShot(attackSound);
                isRange = true;

            }
            rb.velocity = directionNorm * enemy.enemiesSpeed;

        }
        else
        {
            isRange = false;
            rb.velocity = Vector2.zero;

        }
    }


}
