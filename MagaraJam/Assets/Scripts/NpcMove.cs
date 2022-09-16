using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float delayTime;
    [SerializeField] Animator anim;
    bool isMoving;
    Rigidbody rb;
    float firstSpeed;

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
        rb.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);
        speed = 0;
        anim.SetInteger("animState", 0);
        yield return new WaitForSeconds(2);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(2);
        speed = 0;
        anim.SetInteger("animState", 0);
        yield return new WaitForSeconds(2);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);
        speed = 0;
        anim.SetInteger("animState", 0);
        yield return new WaitForSeconds(2);
        anim.SetInteger("animState", 1);
        speed = firstSpeed;
        rb.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(2);
        isMoving = false;
    }
}
