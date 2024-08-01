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
    public int tutorialIndex = 0;

    public float durationTutorial;

    [Header("BeginnerTutorial")]
    public GameObject NotifButton;
    public GameObject Phase1Tutorial;
    public GameObject Phase2Tutorial;
    public GameObject Phase3Tutorial;
    public GameObject TrashTutorial;
    public GameObject DangerTutorial;

    [Header("TutorialButton")]
    public GameObject Phase1Button;
    public GameObject Phase2Button;
    public GameObject Phase3Button;
    public GameObject TrashButton;
    public GameObject DangerButton;



    // public GameObject MoveTutorial;
    // public GameObject TrashTutorial;
    // public GameObject ChestTutorial;
    // public GameObject BombTutorial;
    // public GameObject SharkTutorial;
    // public GameObject TreasureTutorial;
  

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        Phase1Tutorial.SetActive(true);
        Phase1Button.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(tutorialIndex);
    }

    public void TurnOffPhase1()
    {
        Time.timeScale = 0f;
        Phase1Tutorial.SetActive(false);
        Phase1Button.SetActive(false);
        Phase2Button.SetActive(true);
        Phase2Tutorial.SetActive(true);
        
    }

    public void TurnOffPhase2()
    {
        Time.timeScale = 0f;
        Phase2Tutorial.SetActive(false);
        Phase2Button.SetActive(false);
        Phase3Tutorial.SetActive(true);
        Phase3Button.SetActive(true);
    }

    public void TurnOffPhase3()
    {
        Time.timeScale = 0f;
        Phase3Tutorial.SetActive(false);
        Phase3Button.SetActive(false);
        TrashTutorial.SetActive(true);
        TrashButton.SetActive(true);
    }

    public void TurnOffTrash()
    {
        TrashTutorial.SetActive(false);
        Time.timeScale = 1f;
        NotifButton.SetActive(false);
    }

    public void TurnOnDanger()
    {
        DangerTutorial.SetActive(true);
        Time.timeScale = 0f;
    
    }

    public void TurnOffDanger()
    {
        DangerTutorial.SetActive(false);
        Time.timeScale = 1f;
        NotifButton.SetActive(false);
    }

    public void TurnOnChest()
    {
        DangerTutorial.SetActive(true);
        Time.timeScale = 0f;
    
    }

    public void TurnOffChest()
    {
        DangerTutorial.SetActive(false);
        Time.timeScale = 1f;
        NotifButton.SetActive(false);
    }

    public int GetTutorialIndex()
    {
        return tutorialIndex;
    }
    // public void TurnOffBomb()
    // {
    //     BombTutorial.SetActive(false);
    //     Time.timeScale = 1f;
    //     BombTutor = false;
    // }

    // public void TurnOffShark()
    // {
    //     SharkTutorial.SetActive(false);
    //     Time.timeScale = 1f;
    //     SharkTutor = false;
    // }

    // public void TurnOffTreasure()
    // {
    //     TreasureTutorial.SetActive(false);
    //     Time.timeScale = 1f;
    //     TreasureTutor = false;
    // }
}
