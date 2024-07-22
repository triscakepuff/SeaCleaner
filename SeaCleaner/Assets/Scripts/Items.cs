using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(gameObject.tag == "Oxygen")
                {
                    Debug.Log("Item 1");
                }
                else if(gameObject.tag == "Shield")
                {
                    Debug.Log("Item 1");
                }
                else if(gameObject.tag == "Arrow")
                {
                    Debug.Log("Item 1");
                }
                else if(gameObject.tag == "Speed")
                {
                    Debug.Log("Item 1");
                }
            }
            
        }
    }
}
