using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControllerPatched : MonoBehaviour
{


    [SerializeField] float speed;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Transform swordPos;
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI npcText;
    [SerializeField] TextMeshProUGUI angelText;
    [SerializeField] GameObject[] tasks;
    [SerializeField] GameObject gate;
    float NPCcount;
    float angelCount;
    float attackTime;
    float garryTime;
    Rigidbody rb;
    PlayerSound soundSc;
    int isOnStone;

    bool isWalk;
    int taskCounter;

    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        rb = GetComponent<Rigidbody>();
        soundSc = GetComponent<PlayerSound>();
        garryTime = 4;
        npcText.text = NPCcount.ToString();
        angelText.text = angelCount.ToString();

    }
    public void AngelCountFunc()
    {
        angelCount++;
        angelText.text = angelCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Move(horizontalInput, verticalInput);
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && Time.time > attackTime)
        {
            Attack();
        }
        if (Time.time > garryTime)
        {
            soundSc.GarrySound();
            garryTime = Time.time + 15;
        }
        if (angelCount >= 20)
        {
            taskCounter++;
            tasks[1].SetActive(false);
        }
        if (NPCcount >= 30)
        {
            taskCounter++;
            tasks[0].SetActive(false);
        }
        if (taskCounter == 2)
        {
            gate.GetComponent<Collider>().enabled = false;
        }

    }
    IEnumerator Walk()
    {
        yield return new WaitForSeconds(0.25f);
        if (isOnStone == 0)
        {
            soundSc.stoneStepSound();
        }
        else if (isOnStone == 1)
        {
            soundSc.PlaneStepSound();
        }
        else
        {
            soundSc.StopStepSound();
        }
        isWalk = false;
    }
    void Move(float horizontal, float vertical)
    {

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);

        if (rb.velocity != new Vector3(0, rb.velocity.y, 0))
        {
            anim.SetInteger("animState", 1);
            if (!isWalk)
            {
                isWalk = true;
                StartCoroutine("Walk");

            }

        }
        else
        {
            anim.SetInteger("animState", 0);
            soundSc.StopStepSound();
            StopCoroutine("Walk");
            isWalk = false;
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
        attackTime = Time.time + 1 / attackSpeed;
        anim.SetTrigger("attack");
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        soundSc.AttackSound();
        Collider[] enemies = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach (Collider enemy in enemies)
        {
            soundSc.PunchSound();
            enemy.GetComponent<Enemy>().TakeDamage(1);
        }
        Collider[] npcler = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("NPC"));
        foreach (Collider npc in npcler)
        {
            soundSc.PunchSound();
            npc.GetComponent<NpcMove>().TakeDamage(transform);
            NPCcount++;
            npcText.text = NPCcount.ToString();
        }
        Collider[] vases = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("Vase"));
        foreach (Collider vase in vases)
        {
            soundSc.PunchSound();
            soundSc.VaseBreakSound();
            Vector3 direct = vase.transform.position - transform.position;
            vase.GetComponent<Rigidbody>().AddForce(direct.normalized * 300, ForceMode.Impulse);
        }
        Collider[] barels = Physics.OverlapSphere(swordPos.position, attackRange, LayerMask.GetMask("Barrel"));
        foreach (Collider barel in barels)
        {
            soundSc.PunchSound();
            soundSc.BarrelSound();
            Vector3 direct = barel.transform.position - transform.position;
            barel.GetComponent<Rigidbody>().AddForce(direct.normalized * 300, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(.4f);
        particle.Play();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(swordPos.position, attackRange);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            isOnStone = 2;
        }
        if (collision.gameObject.tag == "Plane")
        {
            isOnStone = 2;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Stone")
        {
            isOnStone = 0;
        }
        if (other.gameObject.tag == "Plane")
        {
            isOnStone = 1;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gapı")
        {
            SceneManager.LoadScene("2");

        }
    }




}
