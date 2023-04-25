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
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        audioSource.clip = soundVariations[Random.Range(0, soundVariations.Length)];
    }
}
