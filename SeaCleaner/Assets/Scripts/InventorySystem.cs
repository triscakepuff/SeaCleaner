using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public RectTransform[] inventorySlot;
    public TextMeshProUGUI[] inventoryItemCountText;
    public int[] inventoryItemCount;

    public int inventorySelect;

    private PowerUpManager powerUpManager;
    private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = GameObject.FindFirstObjectByType<PowerUpManager>();
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        inventoryItemCount[0] = 2 + saveManager.gameData.UpgradeArrow;
        inventoryItemCount[1] = saveManager.gameData.UpgradeShield;
        inventoryItemCount[2] = saveManager.gameData.UpgradeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        inventoryItemCountText[0].text = inventoryItemCount[0].ToString();
        inventoryItemCountText[1].text = inventoryItemCount[1].ToString();
        inventoryItemCountText[2].text = inventoryItemCount[2].ToString();

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            // Convert scroll input to integer change
            int change = Mathf.RoundToInt(scrollInput * 10);

            // Update the variableNumber
            inventorySelect += change;

            Debug.Log("Variable Number: " + inventorySelect);
        }

        if(inventorySelect > 3)
        {
            inventorySelect = 0;
        }

        if(inventorySelect < 0)
        {
            inventorySelect = 3;
        }

        if(inventorySelect == 0)
        {
            for (int i = 1; i <= 3; i++)
            {
                inventorySlot[i-1].localScale = new Vector3(1f, 1f, 1f);
                
            }
        }
        else
        {
            for (int i = 1; i <= 3; i++)
            {
                if (i == inventorySelect)
                {
                    inventorySlot[i-1].localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }
                else
                {
                    inventorySlot[i-1].localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
        
        if(inventorySelect == 2 && !powerUpManager.shieldActive && inventoryItemCount[1] > 0 && Input.GetMouseButtonUp(0))
        {
            inventoryItemCount[1]--;
            powerUpManager.shieldActive = true;
        }

        if (inventorySelect == 3 && !powerUpManager.speedActive && inventoryItemCount[2] > 0 && Input.GetMouseButtonUp(0))
        {
            inventoryItemCount[2]--;
            powerUpManager.speedActive = true;
        }
    }
}
