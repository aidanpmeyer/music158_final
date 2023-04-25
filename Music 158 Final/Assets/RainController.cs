using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public AudioSource dropSound; // Assign the raindrop sound effect to this variable

    private ParticleSystem rainParticles; // Reference to the particle system component

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
        if (other.CompareTag("Water"))
        {
            List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
            int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(rainParticles, other, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 collisionPos = collisionEvents[i].intersection;
                dropSound.transform.position = collisionPos;
                dropSound.Play();
            }
        }
    }
}