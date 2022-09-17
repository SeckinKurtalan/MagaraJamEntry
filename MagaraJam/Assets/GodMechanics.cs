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

    public Material myMaterial1;

    void Start()
    {
        InvokeRepeating("GodAttackLevel1", 4f,6f);
        shootingAnim.Stop();
        shootingAnim1.Stop();
        shootingAnim2.Stop();

    }


    void Update()
    {
    
    }

    public void GodAttackLevel1()
    {
        StartCoroutine(GodPunchLevel1());
    }
    
    IEnumerator GodPunchLevel1()
    {

        Color color = myMaterial1.color;
        color.a = 0.8f;
        myMaterial1.color = color;
        animator.SetTrigger("GodPunch"); 
        yield return new WaitForSecondsRealtime(time);
        color.a = 0f;
        myMaterial1.color = color;
        shootingAnim.Play();
        shootingAnim1.Play();
        shootingAnim2.Play();
        
        
    }
   


    

}
