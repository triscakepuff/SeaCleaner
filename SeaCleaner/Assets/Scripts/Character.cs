using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject gameManager;

    public Vector2 startingPosition;
    private float time;
    private bool isDead = false;

    [Header ("Movement")]
    public float swimSpeed = 10f;
    public float waterDrag = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Harpoon harpoonScript;

    private bool isMoving = false;

    public GameObject HarpoonArm;
    private SpriteRenderer HarpoonArmSprite;
    private float HarpoonArmX;
    private float HarpoonArmY;
    public float offsetX;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        HarpoonArmSprite = HarpoonArm.GetComponent<SpriteRenderer>();
        harpoonScript = GetComponent<Harpoon>();
        HarpoonArmX = HarpoonArm.transform.localPosition.x;
        HarpoonArmY = HarpoonArm.transform.localPosition.y;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Swim();
        if(isDead && Input.GetMouseButtonDown(0))
        {
            Respawn();
            isDead = false;
        }

        HarpoonCheck();
    }

    void HarpoonCheck()
    {
        if (harpoonScript.HarpoonActive)
        {
            

            HarpoonArm.SetActive(true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;  // Since this is a 2D game, we keep the z coordinate at 0

            // Calculate the direction from the player to the mouse position
            Vector3 directionToMouse = mousePosition - transform.position;

            // Calculate the direction
            Vector3 fixedDirection = directionToMouse.normalized;

            float angle = Mathf.Atan2(fixedDirection.x, fixedDirection.y) * Mathf.Rad2Deg;

            if (fixedDirection.x < 0)
            {
                spriteRenderer.flipX = false;
                HarpoonArmSprite.flipX = false;
                HarpoonArm.transform.rotation = Quaternion.Euler(HarpoonArm.transform.rotation.x, HarpoonArm.transform.rotation.y, -(angle + 90));
            }
            else if (fixedDirection.x > 0)
            {
                spriteRenderer.flipX = true;
                HarpoonArmSprite.flipX = true;
                HarpoonArm.transform.rotation = Quaternion.Euler(HarpoonArm.transform.rotation.x, HarpoonArm.transform.rotation.y, -(angle - 90));
            }

            if (!isMoving)
            {
                HarpoonArm.transform.localPosition = new Vector3(HarpoonArmX, HarpoonArmY);
            }
            else
            {
                if(fixedDirection.x < 0)
                {
                    HarpoonArm.transform.localPosition = new Vector3(HarpoonArmX + offsetX, HarpoonArmY + offsetY);
                }else if(fixedDirection.x > 0)
                {
                    HarpoonArm.transform.localPosition = new Vector3(HarpoonArmX + -offsetX, HarpoonArmY + offsetY);
                }
                
            }

        }
        else
        {
            HarpoonArm.SetActive(false);
        }
    }

    void Swim()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement
        Vector2 movement = new Vector2(moveHorizontal * swimSpeed, moveVertical * swimSpeed);

        rb.drag = waterDrag;
        rb.velocity = movement;

        if(moveHorizontal > 0 && !harpoonScript.HarpoonActive)
        {
            spriteRenderer.flipX = true;
        }
        else if(moveHorizontal < 0 && !harpoonScript.HarpoonActive)
        {
            spriteRenderer.flipX = false;
        }

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            animator.SetFloat("MoveState", 1);
            isMoving = true;
        }
        else
        {
            animator.SetFloat("MoveState", 0);
            isMoving = false;
        }
    }

    void Death()
    {
        time += Time.deltaTime;
        if(time < 2f)
        {
            isDead = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Respawn()
    {
        transform.position = startingPosition;
        rb.constraints &=  ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation);
    }
}
