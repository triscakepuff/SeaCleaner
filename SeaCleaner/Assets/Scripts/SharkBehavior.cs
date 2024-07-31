using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehavior : MonoBehaviour
{
    [Header("Detection Area")]
    public float smallSphereRadius = 5f;  // Radius of the smaller spherecast
    public float largeSphereRadius = 12f; // Radius of the larger spherecast
    public LayerMask layerMask;           // Layer mask to filter the objects the spherecast will detect

    [Header("Choose 1 (Random Patrol or Fixed)")]

    public bool RandomPatrol = false;
    public bool FixedPatrol = true;

    [Header("Random Patrol")]
    public float minMoveDuration = 5f;
    public float maxMoveDuration = 6f;

    [Header("Fixed Patrol")]
    public float horizontalBound = 5f;

    [Header("Chase/Attack")]

    public float chaseCooldown = 2f;

    private float chosenMoveDuration;
    private float aggroDuration;
    private float aggroCooldown;

    [Header("Speed")]

    public float speedPatrol = 0.5f;
    public float speedAggro = 3f;

    [Header("Check if player is detected")]

    public bool playerDetected = false;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private SpriteRenderer sprite;
    private Vector2 currentPlayerPos;

    private bool FixedMoveLeft = true;
    private float initialPosition;
    private Vector2 initialSharkPos;
    private bool BackToOriginalPos = true;

    private bool isChaseCooldown;
   
    private float sizeX;
    private float sizeY;

    private float fadeDuration = 2f;

    public Sprite sharkAggro;
    public Sprite sharkNotAggro;
    public GameObject sharkAggroSign;
    void Start()
    {
        sizeY = transform.localScale.y;
        sizeX = transform.localScale.x;
        initialSharkPos = transform.position;
        initialPosition = transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectPlayer();

        if (RandomPatrol)
        {
            Countdown();
        }

        if (FixedPatrol && BackToOriginalPos && !playerDetected)
        {
            FixedPatrolHorizontal();
            RotateShark();
        }else if (FixedPatrol && !BackToOriginalPos && !playerDetected)
        {
            GoBackAfterChase();
            RotateShark();
        }

        AggroDuration();

        if (!playerDetected)
        {
            sprite.sprite = sharkNotAggro;
            rb2d.velocity = currentDirection * speedPatrol;
        }else if (playerDetected)
        {
            BackToOriginalPos = false;
            int onCooldown;
            if (isChaseCooldown)
            {
                sprite.sprite = sharkNotAggro;
                onCooldown = 0;
            }
            else
            {
                sprite.sprite = sharkAggro;
                onCooldown = 1;
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

    void FixedPatrolHorizontal()
    {
        if(FixedMoveLeft)
        {
            currentDirection = new Vector2(-1, 0);
        }
        else
        {
            currentDirection = new Vector2(1, 0);
        }

        if(transform.position.x < initialPosition - horizontalBound)
        {
            FixedMoveLeft = false;
        }else if(transform.position.x > initialPosition + horizontalBound)
        {
            FixedMoveLeft = true;
        }
    }

    void GoBackAfterChase()
    {
        currentDirection = (initialSharkPos - (Vector2)transform.position).normalized;
        float distance = Vector3.Distance(initialSharkPos, transform.position);
        if(distance < 0.01f)
        {
            BackToOriginalPos = true;
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
            transform.localScale = new Vector3(-sizeX, sizeY);
        }
        else if (DirToRotate.x < 0) // Moving left
        {
            transform.localScale = new Vector3(-sizeX, -sizeY);
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
            transform.localScale = new Vector3(-sizeX, sizeY);
        }
        else if (currentDirection.x < 0) // Moving left
        {
            transform.localScale = new Vector3(-sizeX, -sizeY);
        }

        // Rotate the shark towards the movement direction
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        rb2d.rotation = angle;
    }

    void DetectPlayer()
    {
        // Perform the smaller overlap circle
        Collider2D[] smallHits = Physics2D.OverlapCircleAll(transform.position, smallSphereRadius, layerMask);

        // Perform the larger overlap circle
        Collider2D[] largeHits = Physics2D.OverlapCircleAll(transform.position, largeSphereRadius, layerMask);

        if (!playerDetected)
        {
            // Check if the small overlap circle hit an object with the "Player" tag
            foreach (var hit in smallHits)
            {
                if (hit.CompareTag("Player"))
                {
                    playerDetected = true;
                    break;
                }
            }
        }
        else
        {
            // Check if the large overlap circle did not hit an object with the "Player" tag
            bool playerStillInLargeSphere = false;
            foreach (var hit in largeHits)
            {
                if (hit.CompareTag("Player"))
                {
                    playerStillInLargeSphere = true;
                    break;
                }
            }

            if (!playerStillInLargeSphere)
            {
                playerDetected = false;
            }
        }

        if (playerDetected)
        {
            sharkAggroSign.SetActive(true);
        }
        else
        {
            sharkAggroSign.SetActive(false);
        }
    }

    public void DeathShark()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        Color originalColor = sprite.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            sprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the alpha is set to 0 after fading is complete
        sprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
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
