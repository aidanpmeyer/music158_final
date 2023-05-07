using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropplet : MonoBehaviour
{
    // Array of AudioClip variations
    public AudioClip[] soundVariations;

    // Reference to the AudioSource component
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource= GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundVariations[Random.Range(0, soundVariations.Length)]);
    }

}
