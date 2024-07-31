using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HarpoonProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public delegate void DestroyCallback();
    public event DestroyCallback OnDestroyCallback;
    public float speed = 8f;

    public float tolerance = 0.1f; // The distance tolerance for the position check

    private bool hits = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero && !hits)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust angle by -90 degrees
        }

        if (hits)
        {
            rb.bodyType = RigidbodyType2D.Static;
            Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

            // Move the transform
            transform.position += direction * speed * Time.deltaTime;
        }

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform != null)
        {
            // Calculate the distance between the object and the player
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Check if the distance is within the tolerance range
            if (distance <= tolerance)
            {
                Destroy(gameObject);
            }
        }

    }

    void OnDestroy()
    {
        OnDestroyCallback?.Invoke(); // Notify that the projectile is destroyed
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shark"))
        {
            collision.gameObject.GetComponent<SharkBehavior>().DeathShark();
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Shark"))
        {
            hits = true;
            rb.velocity = Vector2.zero;
        }
    }

}
