using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource walkSource;
    [SerializeField] AudioClip[] attackSound;
    [SerializeField] AudioClip[] hurtSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip[] stoneWalkSound;
    [SerializeField] AudioClip[] planeWalkSound;
    [SerializeField] AudioClip fallSound;
    private int i = 0; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(attackSound[Random.Range(0, attackSound.Length)]);
    }
    public void HurtSound()
    {
        audioSource.PlayOneShot(hurtSound[Random.Range(0, hurtSound.Length)]);
    }
    public void DieSound()
    {
        audioSource.PlayOneShot(dieSound);
    }
    public void stoneStepSound()
    {
        i++;
        if (i == 2) { i = 0; }
        walkSource.PlayOneShot(stoneWalkSound[i]);
    }
    public void StopStepSound()
    {
        walkSource.Stop();
    }
    public void PlaneStepSound()
    {
        i++;
        if (i == 2) { i = 0; }
        walkSource.PlayOneShot(planeWalkSound[i]);

    }
    public void FallSound()
    {
        audioSource.PlayOneShot(fallSound);
    }
}
