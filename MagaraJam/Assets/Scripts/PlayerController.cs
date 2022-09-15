using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] Transform swordPos;
    float attackTime;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
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
        rb.velocity = new Vector3(horizontal, 0, vertical) * speed;
        if (horizontal >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (vertical > 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (vertical < 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (vertical > 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (vertical < 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }

    }
    void Attack()
    {
        attackTime = Time.time + 1 / attackSpeed;
        Collider[] enemies = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(swordPos.position, attackRange);
    }
}