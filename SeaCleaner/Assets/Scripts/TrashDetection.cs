using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDetection : MonoBehaviour
{
    public bool withinRange;
    public GameObject gameManager;
    private GameObject targetObject;
    void Update()
    {
        if(targetObject != null && Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.SendMessage("TrashCount");
            Destroy(gameObject);
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
