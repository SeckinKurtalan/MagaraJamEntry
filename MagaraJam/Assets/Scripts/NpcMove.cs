using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float delayTime;
    [SerializeField] float speedTime;
    [SerializeField] Animator anim;
    [SerializeField] float firstRotY;
    [SerializeField] float knockBackPower;
    [SerializeField] float knockBackTime;
    bool isMoving;
    Rigidbody rb;
    float firstSpeed;
    bool isHurt;
    bool isForceEnd;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        firstSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (!isMoving && !isHurt)
        {
            isMoving = true;
            StartCoroutine("Move");
        }
        else if (isHurt)
        {
            if (!isForceEnd)
            {
                rb.AddForce(dir.normalized * knockBackPower, ForceMode.Impulse);
            }

        }
    }
    IEnumerator Move()
    {
        speed = 0;
        anim.SetInteger("animState", 0);

        yield return new WaitForSeconds(delayTime);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, firstRotY, 0);

        yield return new WaitForSeconds(speedTime);
        speed = 0;
        anim.SetInteger("animState", 0);

        yield return new WaitForSeconds(delayTime);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, firstRotY - 180, 0);

        yield return new WaitForSeconds(speedTime);
        speed = 0;
        anim.SetInteger("animState", 0);

        yield return new WaitForSeconds(delayTime);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, firstRotY, 0);

        yield return new WaitForSeconds(speedTime);
        speed = 0;
        anim.SetInteger("animState", 0);

        yield return new WaitForSeconds(delayTime);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, firstRotY - 180, 0);

        yield return new WaitForSeconds(speedTime);
        isMoving = false;
    }
    public void TakeDamage(Transform playerPos)
    {
        StopCoroutine("Move");
        isForceEnd = false;
        rb.velocity = Vector3.zero;
        anim.SetInteger("animState", 1);
        dir = transform.position - playerPos.position;
        dir.y = 0;
        speed = 0;
        isHurt = true;
        StartCoroutine(Hurt());
    }
    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(knockBackTime);
        isForceEnd = true;
        anim.SetInteger("animState", 0);
        yield return new WaitForSeconds(2f);
        isHurt = false;
        isMoving = false;
        isForceEnd = false;
    }

}
