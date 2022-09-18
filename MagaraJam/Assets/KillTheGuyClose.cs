using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheGuyClose : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthForGod>().Die();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
