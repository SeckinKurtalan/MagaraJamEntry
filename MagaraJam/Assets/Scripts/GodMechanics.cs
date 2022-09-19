using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GodMechanics : MonoBehaviour
{

    bool isGodDead = false;

    [SerializeField] GameObject FinalArea;

    [SerializeField] GameObject[] level3Areas;

    [SerializeField] GameObject[] level5Areas;

    [SerializeField] PlayerHealthForGod playerHealtScript;

    [SerializeField] GameObject god;

    [SerializeField] GameObject Redzone1;

    [SerializeField] GameObject Redzone2;

    bool redzoneTouchStatus = false;

    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem shootingAnimLevel1;

    [SerializeField] ParticleSystem shootingAnim1Level1;

    [SerializeField] ParticleSystem shootingAnim2Level1;

    [SerializeField] ParticleSystem shootingAnimLevel2;

    [SerializeField] ParticleSystem shootingAnim1Level2;

    [SerializeField] ParticleSystem shootingAnim2Level2;

    [SerializeField] ParticleSystem shootingAnim3Level2;

    [SerializeField] ParticleSystem shootingAnim4Level2;

    public UnityEvent shootingEvent;

    public float time;

    [SerializeField] Material Level1Material;

    [SerializeField] Material Level2Material;

    [SerializeField] GodSound godSound;

    [SerializeField] PlayerControllerForGod playerTouchControllerScript;

    private bool damageNotGiven = true;

    private bool animActive = false;

    int holdNumber = 10;

    ParticleSystem level3Anim;

    void Start()
    {
        shootingAnimLevel1.Stop();
        shootingAnim1Level1.Stop();
        shootingAnim2Level1.Stop();
        shootingAnimLevel2.Stop();
        shootingAnim1Level2.Stop();
        shootingAnim2Level2.Stop();
        shootingAnim3Level2.Stop();
        shootingAnim4Level2.Stop();

        Invoke("RandomAttack", 2f);
        Invoke("RandomAttack", 8f);
        Invoke("RandomAttack", 14f);
        Invoke("RandomAttack", 20f);
        Invoke("RandomAttack", 26f);
        Invoke("RandomAttack", 32f);
        Invoke("RandomAttack", 38f);
        Invoke("RandomAttack", 44f);
        Invoke("StartFinalAttack", 50f);

    }


    void StartFinalAttack()
    {

        StartCoroutine(FinalAttack());
    }


    IEnumerator FinalAttack()
    {
        FinalArea.SetActive(true);
        yield return new WaitForSeconds(2f);
        FinalArea.transform.Find("NukeExplosionFire").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerHealtScript.Die();
    }



    void RandomAttack()
    {
        int random = Random.Range(0, 5);
        if (random == 0)
        {
            GodAttackLevel1();
        }
        else if (random == 1)
        {
            GodAttackLevel2();
        }
        else if (random == 2)
        {
            GodAttackLevel3();
        }
        else if (random == 3)
        {
            GodAttackLevel4();
        }
        else if (random == 4)
        {
            GodAttackLevel5();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }

    public void Update()
    {
        TouchStatusApplier();
        TouchStatusControl();

    }

    public void GodAttackLevel1()
    {
        StartCoroutine(GodPunchLevel1());
    }


    public void GodAttackLevel2()
    {
        StartCoroutine(GodPunchLevel2());
    }

    public void GodAttackLevel3()
    {
        StartCoroutine(GodPunchLevel3());
    }

    public void GodAttackLevel4()
    {
        StartCoroutine(GodPunchLevel4());
    }

    public void GodAttackLevel5()
    {
        StartCoroutine(GodPunchLevel5());
    }

    IEnumerator GodPunchLevel1()
    {

        Redzone1.SetActive(true);
        Color color = Level1Material.color;
        color.a = 0.8f;
        Level1Material.color = color;
        animator.SetTrigger("GodPunch");
        yield return new WaitForSeconds(time);
        color.a = 0f;
        Level1Material.color = color;
        shootingAnimLevel1.Play();
        shootingAnim1Level1.Play();
        shootingAnim2Level1.Play();
        godSound.lazerSoundPlay();
        animActive = true;
        yield return new WaitForSeconds(1f);
        damageNotGiven = true;
        animActive = false;
        redzoneTouchStatus = false;
        Redzone1.SetActive(false);
        //GodAttackLevel2();
    }

    IEnumerator GodPunchLevel2()
    {
        redzoneTouchStatus = false;
        Redzone2.SetActive(true);
        Color color = Level2Material.color;
        color.a = 0.8f;
        Level2Material.color = color;
        animator.SetTrigger("GodPunch");
        yield return new WaitForSeconds(time);
        color.a = 0f;
        Level2Material.color = color;
        shootingAnimLevel2.Play();
        shootingAnim1Level2.Play();
        shootingAnim2Level2.Play();
        shootingAnim3Level2.Play();
        shootingAnim4Level2.Play();
        godSound.lazerSoundPlay();
        animActive = true;
        yield return new WaitForSeconds(1f);
        damageNotGiven = true;
        animActive = false;
        redzoneTouchStatus = false;
        Redzone2.SetActive(false);
        //GodAttackLevel3();
    }


    IEnumerator GodPunchLevel3()
    {
        redzoneTouchStatus = false;
        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, level3Areas.Length);
            if (holdNumber == random)
            {
                while (holdNumber == random)
                {
                    random = Random.Range(0, level3Areas.Length);
                }
            }
            level3Areas[random].SetActive(true);
            StartCoroutine(timer(random));
            holdNumber = random;
        }
        yield return new WaitForSeconds(0f);
        //GodAttackLevel4();
    }


    IEnumerator timer(int random)
    {
        yield return new WaitForSeconds(3f);
        level3Areas[random].transform.Find("NukeExplosionFire").gameObject.SetActive(true);
        if (redzoneTouchStatus)
        {
            playerHealtScript.Die();
        }
        yield return new WaitForSeconds(1f);
        level3Areas[random].transform.Find("NukeExplosionFire").gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        level3Areas[random].SetActive(false);
        redzoneTouchStatus = false;
    }

    IEnumerator GodPunchLevel4()
    {
        redzoneTouchStatus = false;
        for (int i = 0; i < 6; i++)
        {
            int random = Random.Range(0, level3Areas.Length);
            if (holdNumber == random)
            {
                while (holdNumber == random)
                {
                    random = Random.Range(0, level3Areas.Length);
                }
            }
            level3Areas[random].SetActive(true);
            StartCoroutine(timer(random));
            holdNumber = random;
        }
        yield return new WaitForSeconds(6f);
        //GodAttackLevel5();
    }


    IEnumerator GodPunchLevel5()
    {
        redzoneTouchStatus = false;

        int random = Random.Range(0, level5Areas.Length);
        level5Areas[random].SetActive(true);
        yield return new WaitForSeconds(2f);
        level5Areas[random].transform.Find("NukeConeExplosionFire").gameObject.SetActive(true);
        if (redzoneTouchStatus)
        {
            playerHealtScript.Die();
        }

        yield return new WaitForSeconds(1f);
        level5Areas[random].transform.Find("NukeConeExplosionFire").gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        level5Areas[random].SetActive(false);
        redzoneTouchStatus = false;
        yield return new WaitForSeconds(6f);
    }


    void TouchStatusApplier()
    {
        redzoneTouchStatus = playerTouchControllerScript.touchStatus;
    }

    void GiveDamageToTheMainCharacter()
    {
        playerHealtScript.health -= 1;
        damageNotGiven = false;
    }

    void TouchStatusControl()
    {
        if (redzoneTouchStatus && damageNotGiven && animActive)
        {
            playerHealtScript.Die();
        }
    }

}
