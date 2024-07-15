using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float swimSpeed = 10f;
    public float waterDrag = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Swim();
      
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
}
