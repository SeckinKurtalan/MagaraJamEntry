using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerControllerPatched : MonoBehaviour
{


    [SerializeField] float speed;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Transform swordPos;
    [SerializeField] Animator anim;
    float attackTime;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Move(horizontalInput, verticalInput);
        if (Input.GetMouseButtonDown(0) && Time.time > attackTime)
        {
            Attack();
        }

    }
    void Move(float horizontal, float vertical)
    {

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);

        if (rb.velocity != Vector3.zero)
        {
            anim.SetInteger("animState", 1);
        }
        else
        {
            anim.SetInteger("animState", 0);
        }
        if (Input.GetKey(KeyCode.D))
        {

            rb.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.rotation = Quaternion.Euler(0, -45, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.rotation = Quaternion.Euler(0, -135, 0);
            }
            else
            {
                rb.rotation = Quaternion.Euler(0, -90, 0);

            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.rotation = Quaternion.Euler(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.rotation = Quaternion.Euler(0, 135, 0);
            }
            else
            {
                rb.rotation = Quaternion.Euler(0, 90, 0);
            }


        }

    }
    void Attack()
    {
        particle.Play();
        attackTime = Time.time + 1 / attackSpeed;
        anim.SetTrigger("attack");
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        Collider[] enemies = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(1);
        }
        Collider[] npcler = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("NPC"));
        foreach (Collider npc in npcler)
        {
            npc.GetComponent<NpcMove>().TakeDamage(transform);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(swordPos.position, attackRange);
    }



}
