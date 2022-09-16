using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GodMechanics : MonoBehaviour
{

    [SerializeField] GameObject god;

    bool isClicked = false;

    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem shootingAnim;

    [SerializeField] ParticleSystem shootingAnim1;

    [SerializeField] ParticleSystem shootingAnim2;

    public UnityEvent shootingEvent;

    public float time;
    
    void Start()
    {
        shootingAnim.Stop();
        shootingAnim1.Stop();
        shootingAnim2.Stop();

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootingEvent.Invoke();
        }
    }

    public void GodAttack()
    {
        StartCoroutine(GodPunch());
    }
    
    IEnumerator GodPunch()
    {
        animator.SetTrigger("GodPunch"); ;
        yield return new WaitForSecondsRealtime(time);
        shootingAnim.Play();
        shootingAnim1.Play();
        shootingAnim2.Play();
    }
   

}
