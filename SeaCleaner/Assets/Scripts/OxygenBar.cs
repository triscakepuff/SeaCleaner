using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [Header("Place this script on a Slider component,")]
    [Header("disable interactable, and remove handle")]

    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenReduceRate = 1f;

    [Header("Call RestoreOxygen() to restore by the amount below")]
    [Header("you can also call RestoreOxygen(*amount of oxygen restored*)")]
    public float oxygenRestoration = 100f;

    private Slider bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bar = GetComponent<Slider>();
        currentOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentOxygen > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }

        currentOxygen = currentOxygen - (oxygenReduceRate * Time.deltaTime);
        bar.value = currentOxygen / maxOxygen;

        if(currentOxygen <= 0)
        {
            Debug.Log("Oxygen is out");
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
}
