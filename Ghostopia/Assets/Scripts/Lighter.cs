using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public GameObject lighter;
    public GameObject flames;
    public bool isOff;

    public bool isOn;



    void Start()
    {
        isOn = false;
        isOff = true;
        flames.SetActive(false);
    }




    void Update()
    {
        if(Input.GetButtonDown("Fire1") && lighter.activeInHierarchy && isOff)
        {
            flames.SetActive(true);
        
            isOn = true;
            isOff = false;
        }

        else if (Input.GetButtonDown("Fire1") && lighter.activeInHierarchy && isOn)
        {
            flames.SetActive(false);
    
            isOn = false;
            isOff = true;
        }



    }
}