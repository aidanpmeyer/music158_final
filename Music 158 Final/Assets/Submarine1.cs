using UnityEngine;

public class Submarine1: MonoBehaviour
{
    public float triggerHeight = 2.0f;
    private bool hasPlayed = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position.x >= triggerHeight && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
        else
        {
            audioSource.Stop();
        }
    }
}
