using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class Check : MonoBehaviour
{
    public bool CheckBuildings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckBuildings)
        {
            CheckBuildings = false;
            GetComponent<SelfCheck>().Check();
        }

    }
}
