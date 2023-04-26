﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public Light light;
    public TMP_Text text;



    public float lifetime = 100;





    private bool on;
    private bool off;


    void Start()
    {
        light = GetComponent<Light>();

        off = true;
        light.enabled = false;

    }



    void Update()
    {
        text.text = lifetime.ToString("0") + "%";


        if(Input.GetButtonDown("Flashlight") && off)
        {
           
            light.enabled = true;
            on = true;
            off = false;
        }

        else if (Input.GetButtonDown("Flashlight") && on)
        {
           
            light.enabled = false;
            on = false;
            off = true;
        }

        if (on)
        {
            lifetime -= 1 * Time.deltaTime;
        }

        if(lifetime <= 0)
        {
            light.enabled = false;
            on = false;
            off = true;
            lifetime = 0;
        }

        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        
       
    



    }

}