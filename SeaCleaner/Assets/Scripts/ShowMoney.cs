using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMoney : MonoBehaviour
{
    private SaveManager saveManager;
    private TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + saveManager.gameData.GetType().GetField("Money").GetValue(saveManager.gameData).ToString();
    }
}
