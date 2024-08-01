using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header ("UI")]
    public GameObject trashesObtainedText;
    public int trashesObtained;
    public int trashesNeeded;

    private void Start()
    {
        trashesObtainedText.GetComponent<TextMeshProUGUI>().text = "Objective: Collect trash (" + trashesObtained.ToString() + "/" + trashesNeeded.ToString() + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TrashCount()
    {
        trashesObtained++;
        trashesObtainedText.GetComponent<TextMeshProUGUI>().text = "Objective: Collect trash (" + trashesObtained.ToString() + "/" + trashesNeeded.ToString() + ")";
        FindObjectOfType<AudioManager>().Play("Trash Interact");
    }

    void BombExplode()
    {
        FindObjectOfType<AudioManager>().Play("Bomb Explosion");

    }

}
