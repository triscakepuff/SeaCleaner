using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{   
    private GameObject targetObject;

    private int randomValue;
    void Update()
    {
       
        if(targetObject != null && Input.GetKeyDown(KeyCode.Space))
        {
           randomValue = Random.Range(0,2);
          
        }
        if(randomValue == 1)
        {
            Debug.Log("Oxygen bar increased!");
        }else if(randomValue == 2)
        {
            Debug.Log("Speed Boost!");
        }else if(randomValue == 3)
        {
            Debug.Log("Shield!");
        }
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("Hi");
        if(other.CompareTag("Player"))
        {
            targetObject = other.gameObject;
        }
    }
    void OnTriggerExit2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            targetObject = other.gameObject;
        }
    }
}
