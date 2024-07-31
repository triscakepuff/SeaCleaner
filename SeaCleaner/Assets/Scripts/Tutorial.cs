using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int posTrashL;
    public int posBombL;
    public int posSharkL;
    public int posTreasureL;
    public int posTrashR;
    public int posBombR;
    public int posSharkR;
    public int posTreasureR;

    public float durationTutorial;

    public GameObject MoveTutorial;
    public GameObject TrashTutorial;
    public GameObject BombTutorial;
    public GameObject SharkTutorial;
    public GameObject TreasureTutorial;

    public GameObject Player;


    private bool TrashTutor = true;
    private bool BombTutor = true;
    private bool SharkTutor = true;
    private bool TreasureTutor = true;
  

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        MoveTutorial.SetActive(true);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.x > posTrashL && Player.transform.position.x < posTrashR && TrashTutor)
        {
            TrashTutorial.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Player.transform.position.x > posBombL && Player.transform.position.x < posBombR && BombTutor)
        {
            BombTutorial.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Player.transform.position.x > posSharkL && Player.transform.position.x < posSharkR && SharkTutor)
        {
            SharkTutorial.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Player.transform.position.x > posTreasureL && Player.transform.position.x < posTreasureR && TreasureTutor)
        {
            TreasureTutorial.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void TurnOffMove()
    {
        MoveTutorial.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TurnOffTrash()
    {
        TrashTutorial.SetActive(false);
        Time.timeScale = 1f;
        TrashTutor = false;
    }

    public void TurnOffBomb()
    {
        BombTutorial.SetActive(false);
        Time.timeScale = 1f;
        BombTutor = false;
    }

    public void TurnOffShark()
    {
        SharkTutorial.SetActive(false);
        Time.timeScale = 1f;
        SharkTutor = false;
    }

    public void TurnOffTreasure()
    {
        TreasureTutorial.SetActive(false);
        Time.timeScale = 1f;
        TreasureTutor = false;
    }
}
