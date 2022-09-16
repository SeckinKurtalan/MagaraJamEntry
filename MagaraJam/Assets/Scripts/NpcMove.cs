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
    bool isMoving;
    Rigidbody rb;
    float firstSpeed;
    bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        firstSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(Move());
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
}
