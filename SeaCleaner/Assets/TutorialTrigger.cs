using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject notification;
    public GameObject dangerTrigger;

   void OnTriggerEnter2D (Collider2D other)
    {
       notification.SetActive(true);
       Destroy(dangerTrigger);
        
    }
}
