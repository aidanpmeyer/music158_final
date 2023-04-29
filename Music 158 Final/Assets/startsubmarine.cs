using UnityEngine;

public class HeightSound : MonoBehaviour
{
    public float triggerHeight = 5.0f;
    private bool hasPlayed = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position.y >= triggerHeight && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
