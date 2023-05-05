using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiveBeat : MonoBehaviour
{
    public float Beat = 4.0f;  // time between audio playback in seconds
    private AudioSource audioSource;

    private Coroutine playbackCoroutine;  // reference to the currently running coroutine
    private bool active = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("called");
            Interact();
        }
    }
    private void Start()
    {
        // get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }


    public void Interact()
    {
        if (!active) { Activate(); } else { Deactivate(); }
    }
    private void Activate()
    {
        
        // stop any previously running coroutine
        if (playbackCoroutine != null)
            StopCoroutine(playbackCoroutine);

        // start a new coroutine that repeatedly plays the audio clip
        playbackCoroutine = StartCoroutine(PlayAudio());
        active= true;
    }

    private void Deactivate()
    {
        // stop the currently running coroutine
        if (playbackCoroutine != null)
            StopCoroutine(playbackCoroutine);
            active= false;
    }

    private IEnumerator PlayAudio()
    {
        // play the audio clip indefinitely with a constant time interval
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(Beat);
        }
    }
}
