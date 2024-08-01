using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Notification : MonoBehaviour
{   
    public Button myButton;
    public GameObject gameManager;
    private Tutorial canvasFunctions;
    // Start is called before the first frame update
    void Start()
    {
        if (myButton == null)
        {
            myButton = GetComponent<Button>();
        }
        gameManager.SendMessage("Notification");

        canvasFunctions = GetComponentInParent<Tutorial>();
        int index = canvasFunctions.GetTutorialIndex();
        if (canvasFunctions != null)
        {
            // Assign the appropriate function based on the condition
            if (index == 0)
            {
               myButton.onClick.AddListener(canvasFunctions.TurnOnDanger);
            }
            else if(index == 1)
            {
                myButton.onClick.AddListener(canvasFunctions.TurnOnChest);
            }
        }
        else
        {
            Debug.LogError("CanvasFunctions script not found in parent Canvas");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
