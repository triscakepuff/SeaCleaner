using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMoneyGiver : MonoBehaviour
{
    public Button button;
    private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        button.onClick.AddListener(GiveMoney);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveMoney()
    {
        int CurrentMoney = (int)saveManager.gameData.GetType().GetField("Money").GetValue(saveManager.gameData);

        saveManager.gameData.GetType().GetField("Money").SetValue(saveManager.gameData, CurrentMoney + 1000);
    }
}
