using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetectionArea : MonoBehaviour
{
    public GameObject character;
    public GameObject gameManager;
    public GameObject bomb;
    public float explosionDelay = 3.0f; // Time in seconds before the bomb explodes
    public float detectionRadius = 5.0f; // Radius within which the player triggers the bomb

    private bool playerInRange = false;
    private bool hasWalkedThrough = false;
    private float countdown;
    private Animator targetAnimator;

    void Start()
    {
        countdown = explosionDelay;
        
    }

    void Update()
    {
        if (playerInRange || hasWalkedThrough)
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
            hasWalkedThrough = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Explode()
    {
        // Add explosion effects here (e.g., particle system, sound, damage to player, etc.)
        gameManager.SendMessage("BombExplode");
        Debug.Log("Boom!");
        targetAnimator = bomb.GetComponent<Animator>();
        targetAnimator.SetTrigger("Explode");
        if(playerInRange) character.SendMessage("Death");
    }
}
