using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimation : MonoBehaviour
{
    public GameObject Bomb;
    public float amplitude = 1f; // The height of the up and down movement
    public float frequency = 1f; // The speed of the up and down movement
    // Start is called before the first frame update
     private Vector3 startPosition;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = startPosition.x + Mathf.Sin(Time.time * frequency) * amplitude;

        // Update the object's position
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);
    }

    public void Explode()
    {
        Destroy(Bomb);
    }
}
