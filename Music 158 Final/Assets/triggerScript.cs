using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SoundObject"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
