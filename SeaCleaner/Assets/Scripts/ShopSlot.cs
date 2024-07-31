using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class ItemData
{
    public string Name;
    public int Price;
}

public class ShopSlot : MonoBehaviour
{
    [Header("Add more if there are more upgrade")]
    public List<ItemData> Item;

    [Header("The ID is the name of the PlayerPrefs that save the buy data")]
    public string id;

    [Header("Be careful with this one.")]

    [Header("SlotReliant means it number of time it can be bought checks slot first")]
    public bool isSlotReliant;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI priceText;
    private Button button;
    private GameObject maxUpgradeBlock;
    private GameObject needMoreSlotBlock;

    private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        nameText = this.transform.Find("UpgradeName").GetComponent<TextMeshProUGUI>();
        priceText = this.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        button = this.transform.Find("BuyBtn").GetComponent<Button>();
        maxUpgradeBlock = this.transform.Find("MaxedUpgrade").gameObject;
        needMoreSlotBlock = this.transform.Find("NeedMoreSlot").gameObject;

        button.onClick.AddListener(BuyItem);

        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int SlotAvailable = 0;

        if (isSlotReliant)
        {
            SlotAvailable = (int)saveManager.gameData.GetType().GetField("UpgradeSlot").GetValue(saveManager.gameData);
        }

        int CurrentUpgrade;

        CurrentUpgrade = (int)saveManager.gameData.GetType().GetField(id).GetValue(saveManager.gameData);
        
        if(CurrentUpgrade < Item.Count)
        {
            nameText.text = Item[CurrentUpgrade].Name.ToString();
            priceText.text = Item[CurrentUpgrade].Price.ToString();
        }
        else
        {
            nameText.text = Item[CurrentUpgrade-1].Name.ToString();
            priceText.text = Item[CurrentUpgrade - 1].Price.ToString();
        }

        if(Item.Count == CurrentUpgrade)
        {
            maxUpgradeBlock.SetActive(true);
        }
        else
        {
            maxUpgradeBlock.SetActive(false);
        }

        if (isSlotReliant && SlotAvailable - 1 < CurrentUpgrade && Item.Count != CurrentUpgrade)
        {
            needMoreSlotBlock.SetActive(true);
        }
        else
        {
            needMoreSlotBlock.SetActive(false);
        }
    }

    public void BuyItem()
    {
        if((int)saveManager.gameData.GetType().GetField("Money").GetValue(saveManager.gameData) > Item[(int)saveManager.gameData.GetType().GetField(id).GetValue(saveManager.gameData)].Price)
        {
            int CurrentMoney = (int)saveManager.gameData.GetType().GetField("Money").GetValue(saveManager.gameData);

            int CurrentUpgrade = (int)saveManager.gameData.GetType().GetField(id).GetValue(saveManager.gameData);
            saveManager.gameData.GetType().GetField("Money").SetValue(saveManager.gameData, CurrentMoney - Item[CurrentUpgrade].Price);

            FieldInfo field = saveManager.gameData.GetType().GetField(id);

            if (field != null)
            {
                int newValue = (int)field.GetValue(saveManager.gameData) + 1;
                field.SetValue(saveManager.gameData, newValue);
            }
            else
            {
                Debug.Log("Error, incorrect id from ShopSlot");
            }
        }
    }
}
