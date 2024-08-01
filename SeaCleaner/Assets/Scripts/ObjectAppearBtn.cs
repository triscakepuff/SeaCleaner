using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppearBtn : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        obj.SetActive(true);
    }

    public void Deactivate()
    {
        obj.SetActive(false);
    }
}
