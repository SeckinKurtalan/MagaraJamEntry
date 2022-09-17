using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip lazerSound;




    public void lazerSoundPlay()
    {
        audioSource.PlayOneShot(lazerSound);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
