using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AngelController : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] float swordRange;
    [SerializeField] Transform swordPos;
    [SerializeField] GameObject playerHead;
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip[] hitSound;
    [SerializeField] Animator anim;
    AudioSource audioSource;
    Enemy enemy;
    Rigidbody rb;
    bool isRange;
    Vector3 direction;
    float attackTime;
    bool isAttack;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isDead)
        {
            direction = playerHead.transform.position - transform.position;
            Vector3 directionNorm = direction.normalized;

            transform.LookAt(playerHead.transform.position);
            transform.rotation = Quaternion.Euler(30, transform.rotation.eulerAngles.y, 0);

            if (direction.magnitude < attackRange)
            {
                if (!isRange)
                {
                    audioSource.PlayOneShot(attackSound);
                    isRange = true;

                }
                if (!isAttack)
                {
                    rb.velocity = directionNorm * enemy.enemiesSpeed;

                }
                else
                {
                    rb.velocity = -directionNorm * enemy.enemiesSpeed;
                }

            }
            else
            {
                isRange = false;
                rb.velocity = Vector2.zero;

            }
            Collider[] hitPlayer = Physics.OverlapSphere(swordPos.position, swordRange, LayerMask.GetMask("Player"));

            if (hitPlayer.Length > 0 && Time.time > attackTime && !enemy.isHurt)
            {
                Attack();
            }
            else if (enemy.isHurt)
            {
                StopCoroutine("AttackTimer");
            }
        }
    }
    void Attack()
    {
        attackTime = Time.time + 2f;
        StartCoroutine("AttackTimer");

    }
    IEnumerator AttackTimer()
    {
        anim.SetTrigger("attack");
        int random = Random.Range(0, hitSound.Length);
        audioSource.PlayOneShot(hitSound[random]);
        yield return new WaitForSeconds(.5f);
        particle.Play();
        Collider[] hitPlayer = Physics.OverlapSphere(swordPos.position, swordRange, LayerMask.GetMask("Player"));

        if (hitPlayer.Length > 0)
        {
            hitPlayer[0].GetComponent<PlayerHealth>().UpdateHealth(1);
        }
        isAttack = true;
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swordPos.position, swordRange);
    }


}
