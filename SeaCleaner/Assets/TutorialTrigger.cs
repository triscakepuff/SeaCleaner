using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject notification;
    public GameObject trashTrigger;
    public GameObject dangerTrigger;

   void OnTriggerEnter2D (Collider2D other)
    {
        if(trashTrigger.name == "TrashTrigger")
        {
            notification.SetActive(true);
            Destroy(trashTrigger);
        }
        if(dangerTrigger.name == "DangerTrigger")
        {
            Debug.Log("HI");
            notification.SetActive(true);
            Destroy(dangerTrigger);
        }
        
    }
}
