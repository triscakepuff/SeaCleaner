using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectionBtn : MonoBehaviour
{
    public GameObject LockSprite;
    public int level = 0;
    private Button btn;
    private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindFirstObjectByType<SaveManager>();
        btn = GetComponent<Button>();

        if(level <= saveManager.gameData.LevelProgress)
        {
            btn.interactable = true;
            LockSprite.SetActive(false);
        }
        else
        {
            btn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
