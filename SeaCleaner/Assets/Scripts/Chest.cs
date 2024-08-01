using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Chest : MonoBehaviour
{   
    private GameObject targetObject;

    public int chestObject;

    public bool isRandom;

    private Animator anim;

    private bool isOpened = false;

    public GameObject[] SpriteObject; 

    private InventorySystem inventorySystem;
    private OxygenBar OxygenBar;
    void Start()
    {
        anim = GetComponent<Animator>();
        inventorySystem = GameObject.FindFirstObjectByType<InventorySystem>();
        OxygenBar = GameObject.FindFirstObjectByType<OxygenBar>();
    }
    void Update()
    {
       
        if(targetObject != null && Input.GetKeyUp(KeyCode.Space) && !isOpened)
        {
            FindFirstObjectByType<AudioManager>().Play("Opening Chest");

            if (isRandom)
            {
                chestObject = Random.Range(0, 2);
            }

            anim.SetTrigger("Opened");

            if(chestObject == 0)
            {
                OxygenBar.currentOxygen += 35;
            }else if(chestObject == 1)
            {
                inventorySystem.inventoryItemCount[1]++;
            }else if (chestObject == 2)
            {
                inventorySystem.inventoryItemCount[2]++;
            }

            SpriteObject[chestObject].SetActive(true);

            StartCoroutine(ChestObject());
            
            isOpened = true;
        }
    }

    IEnumerator ChestObject()
    {
        yield return new WaitForSeconds(1f);

        Color originalColor = SpriteObject[chestObject].GetComponent<SpriteRenderer>().color;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / 1f);
            SpriteObject[chestObject].GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        SpriteObject[chestObject].SetActive(false);
        StopCoroutine(ChestObject());
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("Hi");
        if(other.CompareTag("Player"))
        {
            targetObject = other.gameObject;
        }
    }
    void OnTriggerExit2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            targetObject = null;
        }
    }
}
