using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public GameObject dropSound; // Assign the raindrop sound effect to this variable

    private ParticleSystem rainParticles; // Reference to the particle system component

    private bool pouring = false;

    void Start()
    {
        rainParticles = GetComponent<ParticleSystem>();
        // Set up the collision module to detect collisions with the water plane
        var collision = rainParticles.collision;
        collision.collidesWith = LayerMask.GetMask("Water"); // Set the water plane layer
        collision.enableDynamicColliders = true;
    }

    // Method to be called when a rain particle collides with the water plane
    void OnParticleCollision(GameObject other)
    {
        if (!pouring)
        {
            if (other.CompareTag("Water"))
            {
                List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
                int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(rainParticles, other, collisionEvents);
                for (int i = 0; i < numCollisionEvents; i++)
                {
                    Vector3 collisionPos = collisionEvents[i].intersection;
                    GameObject newDrop = Instantiate(dropSound);
                    newDrop.transform.position = collisionPos;
                    StartCoroutine(Drop(newDrop));
                }
            }
        }
    }
    IEnumerator Drop(GameObject drop)
    {
        AudioSource dropSound = drop.GetComponent<AudioSource>();
        dropSound.Play();
        yield return new WaitForSeconds(1.0f);
        Destroy(drop);

       
    }

    public IEnumerator RainSequence(float DayLength)
    {
        float elapsedTime = 0;
        float startRate = 0f; // starting rain rate
        float endRate = 100f; // ending rain rate
        float currentRate = startRate;
        float rateIncreasePerSecond = (endRate - startRate) / DayLength ;
        var emission = rainParticles.emission;
        Debug.Log("rain starts");
        while (elapsedTime < DayLength)
        {
            // Increase rain rate over time
            currentRate += rateIncreasePerSecond * Time.deltaTime;

            // Set rain rate on rain system
            emission.rateOverTime = currentRate;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final rain rate
        Debug.Log("pouring");
        emission.rateOverTime = 500f;
        pouring = true;
        AudioSource rain = this.GetComponent<AudioSource>();
        rain.Play();
        yield return null;


    }


}