using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GodMechanics : MonoBehaviour
{

    [SerializeField] PlayerHealth playerHealtScript;
    
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
        Invoke("GodAttackLevel1", 4f);
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
    
    IEnumerator GodPunchLevel1()
    {

        Redzone1.SetActive(true);
        Color color = Level1Material.color;
        color.a = 0.8f;
        Level1Material.color = color;
        animator.SetTrigger("GodPunch");
        yield return new WaitForSecondsRealtime(time);
        color.a = 0f;
        Level1Material.color = color;
        shootingAnimLevel1.Play();
        shootingAnim1Level1.Play();
        shootingAnim2Level1.Play();
        godSound.lazerSoundPlay();
        animActive = true;
        yield return new WaitForSecondsRealtime(3f);
        Redzone1.SetActive(false);
        yield return new WaitForSecondsRealtime(5f);
        damageNotGiven = true;
        animActive = false;
        GodAttackLevel2();
    }
   
    IEnumerator GodPunchLevel2()
    {
        Redzone2.SetActive(true);
        Color color = Level2Material.color;
        color.a = 0.8f;
        Level2Material.color = color;
        animator.SetTrigger("GodPunch");
        yield return new WaitForSecondsRealtime(time);
        color.a = 0f;
        Level2Material.color = color;
        shootingAnimLevel2.Play();
        shootingAnim1Level2.Play();
        shootingAnim2Level2.Play();
        shootingAnim3Level2.Play();
        shootingAnim4Level2.Play();
        godSound.lazerSoundPlay();
        animActive = true;
        yield return new WaitForSecondsRealtime(3f);
        damageNotGiven = true;
        animActive = false;
        Redzone2.SetActive(false);
    }

    
    void TouchStatusApplier()
    {
        redzoneTouchStatus = playerTouchControllerScript.touchStatus;
    }

    void GiveDamageToTheMainCharacter()
    {
        playerHealtScript.UpdateHealth(1f);
        damageNotGiven = false;
    }

    void TouchStatusControl()
    {
        if (redzoneTouchStatus && damageNotGiven && animActive)
        {
            GiveDamageToTheMainCharacter();
        }
    }

}
