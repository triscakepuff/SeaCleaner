using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.SendMessage("Notification");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
