using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAngel : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] GameObject playerHead;
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioClip attackSound;
    [SerializeField] Animator anim;
    float attackTime;
    [SerializeField] PlayerHealth playerHealth;
    AudioSource audioSource;
    Vector3 direction;
    Enemy enemy;
    bool isRange;
    bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        audioSource = GetComponent<AudioSource>();
        enemy = GetComponent<Enemy>();
    }

    //Update is called once per frame
    void Update()
    {
        if (!enemy.isDead)
        {
            direction = playerHead.transform.position - transform.position;
            Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red);
            if (!isAttack)
            {
                transform.LookAt(playerHead.transform.position);

            }
            if (direction.magnitude <= attackRange)
            {
                if (!isRange)
                {
                    isRange = true;
                    audioSource.PlayOneShot(attackSound);
                }
                if (Time.time > attackTime)
                {
                    Attack();
                }

            }
            else
            {
                isRange = false;
            }
        }
    }
    void Attack()
    {
        particle.Play();
        attackTime = Time.time + attackSpeed;
        isAttack = true;
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        bool hit = Physics.Raycast(transform.position, transform.forward, attackRange, LayerMask.GetMask("Player"));
        if (hit)
        {
            playerHealth.UpdateHealth(1);
        }
        isAttack = false;
    }
}
