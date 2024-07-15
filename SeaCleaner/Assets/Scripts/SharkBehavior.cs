using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehavior : MonoBehaviour
{
    public float smallSphereRadius = 5f;  // Radius of the smaller spherecast
    public float largeSphereRadius = 12f; // Radius of the larger spherecast
    public LayerMask layerMask;           // Layer mask to filter the objects the spherecast will detect

    public float minMoveDuration = 5f;
    public float maxMoveDuration = 6f;
    public float chaseCooldown = 2f;

    private float chosenMoveDuration;
    private float aggroDuration;
    private float aggroCooldown;


    public float speedPatrol = 0.5f;
    public float speedAggro = 3f;

    public bool playerDetected;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private SpriteRenderer sprite;
    private Vector2 currentPlayerPos;

    private bool isChaseCooldown;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectPlayer();

        Countdown();
        AggroDuration();

        if (!playerDetected)
        {
            rb2d.velocity = currentDirection * speedPatrol;
        }else if (playerDetected)
        {
            int onCooldown;
            if (isChaseCooldown)
            {
                onCooldown = 0;
            }
            else
            {
                onCooldown= 1;
            }
            rb2d.velocity = currentDirection * speedAggro * onCooldown;
        }
    }

    void Countdown()
    {
        if (!playerDetected)
        {
            chosenMoveDuration -= Time.deltaTime;

            if (chosenMoveDuration < 0f)
            {
                Debug.Log("Change Direction!");
                currentDirection = RandomAngle();
                RandomDurationChoose();
                RotateShark();
            }
        }
        else
        {
            chosenMoveDuration = -1f;
        }
    }

    void RandomDurationChoose()
    {
        chosenMoveDuration = Random.Range(minMoveDuration, maxMoveDuration);
    }

    void AggroDuration()
    {
        
        if (playerDetected)
        {
            float distance = Vector3.Distance(currentPlayerPos, transform.position);
            if (aggroCooldown > 0f)
            {
                RotateGetPlayerPos();
                aggroCooldown -= Time.deltaTime;
                isChaseCooldown = true;
            }
            else if(aggroCooldown <= 0f && aggroCooldown >= -5f)
            {
                aggroCooldown -= 5f;
                isChaseCooldown = false;
                currentPlayerPos = GetPlayerPos();
                RotateShark();
            }
            else if(distance < 0.1f)
            {
                aggroCooldown = chaseCooldown;
            }
        }
        else
        {
            aggroCooldown = 1f;
        }
    }

    Vector2 GetPlayerPos()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerObjectTransform = playerObject.transform.position;
        currentDirection = (playerObjectTransform - (Vector2)transform.position).normalized;
        return playerObjectTransform;
    }

    void RotateGetPlayerPos()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerObjectTransform = playerObject.transform.position;
        Vector2 DirToRotate = (playerObjectTransform - (Vector2)transform.position).normalized;
        // Update sprite flipping and rotation based on movement direction
        if (DirToRotate.x > 0) // Moving right
        {
            sprite.flipX = true; // No flip along X-axis
            sprite.flipY = false; // Flip along Y-axis
        }
        else if (DirToRotate.x < 0) // Moving left
        {
            sprite.flipX = true; // No flip along X-axis
            sprite.flipY = true; // Flip along Y-axis
        }

        // Rotate the shark towards the movement direction
        float angle = Mathf.Atan2(DirToRotate.y, DirToRotate.x) * Mathf.Rad2Deg;
        rb2d.rotation = angle;
    }

    Vector2 RandomAngle()
    {
        // Randomly choose between left and right ranges
        bool isRightDirection = Random.value > 0.5f;

        float angle;

        if (isRightDirection)
        {
            // Right: 320° to 360° and 0° to 40°
            float rightRange1 = Random.Range(320f, 360f);
            float rightRange2 = Random.Range(0f, 40f);
            
            if(Random.value > 0.5f)
            {
                angle = rightRange1;
            }
            else
            {
                angle = rightRange2;
            }
        }
        else
        {
            // Left: 120° to 200°
            angle = Random.Range(120f, 200f);
        }

        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
    }

    void RotateShark()
    {
        // Update sprite flipping and rotation based on movement direction
        if (currentDirection.x > 0) // Moving right
        {
            sprite.flipX = true; // No flip along X-axis
            sprite.flipY = false; // Flip along Y-axis
        }
        else if (currentDirection.x < 0) // Moving left
        {
            sprite.flipX = true; // No flip along X-axis
            sprite.flipY = true; // Flip along Y-axis
        }

        // Rotate the shark towards the movement direction
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        rb2d.rotation = angle;
    }

    void DetectPlayer()
    {
        // Perform the smaller spherecast
        RaycastHit2D smallHit = Physics2D.CircleCast(transform.position, smallSphereRadius, Vector2.zero, 0f, layerMask);

        // Perform the larger spherecast
        RaycastHit2D largeHit = Physics2D.CircleCast(transform.position, largeSphereRadius, Vector2.zero, 0f, layerMask);

        if (!playerDetected)
        {
            // Check if the small spherecast hit an object with the "Player" tag
            if (smallHit.collider != null && smallHit.collider.CompareTag("Player"))
            {
                playerDetected = true;
            }
        }
        else
        {
            // Check if the large spherecast did not hit an object with the "Player" tag
            if (largeHit.collider == null || !largeHit.collider.CompareTag("Player"))
            {
                playerDetected = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw the smaller sphere in the scene view for visualization purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallSphereRadius);

        // Draw the larger sphere in the scene view for visualization purposes
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, largeSphereRadius);
    }
}
