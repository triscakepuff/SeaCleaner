using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetectionArea : MonoBehaviour
{
    public GameObject character;
    public float explosionDelay = 3.0f; // Time in seconds before the bomb explodes
    public float detectionRadius = 5.0f; // Radius within which the player triggers the bomb

    private bool playerInRange = false;
    private float countdown;

    void Start()
    {
        countdown = explosionDelay;
        
    }

    void Update()
    {
        if (playerInRange)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Explode();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            countdown = explosionDelay; // Reset countdown if player leaves the range
        }
    }

    void Explode()
    {
        // Add explosion effects here (e.g., particle system, sound, damage to player, etc.)
        Debug.Log("Boom!");
        Destroy(gameObject); // Destroy the bomb object after exploding
        character.SendMessage("Death");
    }
}
