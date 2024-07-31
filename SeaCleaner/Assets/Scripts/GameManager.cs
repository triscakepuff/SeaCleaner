using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header ("UI")]
    public GameObject trashesObtainedText;
    public int trashesObtained;
    // Update is called once per frame
    void Update()
    {
        
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

}
