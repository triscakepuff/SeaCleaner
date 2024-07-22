using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject gameManager;

    private Vector2 startingPosition;
    private float time;
    private bool isDead = false;

    [Header ("Movement")]
    public float swimSpeed = 10f;
    public float waterDrag = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    void Swim()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement
        Vector2 movement = new Vector2(moveHorizontal * swimSpeed, moveVertical * swimSpeed);

        rb.drag = waterDrag;
        rb.velocity = movement;
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
