using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public bool shieldActive = false;
    public bool speedActive = false;

    public float shieldDuration = 6f;
    public float speedDuration = 6f;

    private float shieldDurationCurr;
    private float speedDurationCurr;

    public GameObject ShieldSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountdownShield();
        CountdownSpeed();
    }

    void CountdownShield()
    {
        if (shieldActive)
        {
            ShieldSprite.SetActive(true);
            shieldDurationCurr -= Time.deltaTime;

            if (shieldDurationCurr < 0f)
            {
                shieldActive = false;
            }
        }
        else
        {
            ShieldSprite.SetActive(false);
            shieldDurationCurr = shieldDuration;
        }
    }

    void CountdownSpeed()
    {
        if (speedActive)
        {
            speedDurationCurr -= Time.deltaTime;

            if (speedDurationCurr < 0f)
            {
                speedActive = false;
            }
        }
        else
        {
            speedDurationCurr = speedDuration;
        }
    }
}
