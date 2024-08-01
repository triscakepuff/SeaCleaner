using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    [Header ("UI")]
    public GameObject trashesObtainedText;
    public GameObject winScreen;
    public int trashesObtained;
    public int trashesNeeded;
    private Rigidbody2D rb;

    private SaveManager saveManager;
    public int CoinObtained;
    public int NextLevel;
    private void Start()
    {
        saveManager = FindFirstObjectByType<SaveManager>();
        trashesObtainedText.GetComponent<TextMeshProUGUI>().text = trashesObtained.ToString();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        WinCondition();
    }

    void TrashCount()
    {
        trashesObtained++;
        trashesObtainedText.GetComponent<TextMeshProUGUI>().text = trashesObtained.ToString();
        FindObjectOfType<AudioManager>().Play("Trash Interact");
    }

    void BombExplode()
    {
        FindObjectOfType<AudioManager>().Play("Bomb Explosion");

    }

    void Notification()
    {
        FindObjectOfType<AudioManager>().Play("Notification");
    }

    void OpeningChest()
    {
        FindObjectOfType<AudioManager>().Play("Opening Chest");

    }
    void WinCondition()
    {
        if(trashesObtained == 15)
        {
            trashesObtained = 0;
            saveManager.gameData.Money += CoinObtained;
            saveManager.gameData.LevelProgress = NextLevel;
            saveManager.SaveGameData(saveManager.gameData);
            winScreen.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    
}
