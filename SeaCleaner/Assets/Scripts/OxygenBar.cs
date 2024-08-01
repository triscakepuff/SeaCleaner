using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public GameObject character;
    [Header("Place this script on a Slider component,")]
    [Header("disable interactable, and remove handle")]

    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenReduceRate = 1f;
    private float oxygenPercentage;

    [Header("Call RestoreOxygen() to restore by the amount below")]
    [Header("you can also call RestoreOxygen(*amount of oxygen restored*)")]
    public float oxygenRestoration = 100f;

    [Header("UI")]
    public Slider slider;
    public Image statusImage;
    public Sprite Full;
    public Sprite halfFull;
    public Sprite quarterFull;
    public Sprite none;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMaxOxygen();
        currentOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        SetOxygen();
        CheckOxygenPercentage();
<<<<<<< Updated upstream
=======
        //Debug.Log(oxygenPercentage);
>>>>>>> Stashed changes
        if(currentOxygen > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }

        currentOxygen = currentOxygen - (oxygenReduceRate * Time.deltaTime);

        if(currentOxygen <= 0)
        {
            Debug.Log("Oxygen is out");
           character.SendMessage("Death");
        }
    }

    public void RestoreOxygen()
    {
        currentOxygen = currentOxygen + oxygenRestoration;
    }

    public void RestoreOxygen(float oxygenRestorationCustom)
    {
        currentOxygen = currentOxygen + oxygenRestorationCustom;
    }

    public void SetOxygen()
    {
        slider.value = currentOxygen;
    }

     public void SetMaxOxygen()
    {
        slider.maxValue = maxOxygen;
        slider.value = currentOxygen;
    }
    public void CheckOxygenPercentage()
    {
        oxygenPercentage = (currentOxygen / maxOxygen) * 100;
        if(oxygenPercentage <= 100f && oxygenPercentage > 50f)
        {
            statusImage.sprite = Full;
        }else if((oxygenPercentage <= 50f && oxygenPercentage > 25f))
        {
            statusImage.sprite = halfFull;
        }else if((oxygenPercentage <= 25f && oxygenPercentage > 0f))
        {
            statusImage.sprite = quarterFull;
        }else if(oxygenPercentage <= 0f)
        {
            statusImage.sprite = none;
        }
    }
}
