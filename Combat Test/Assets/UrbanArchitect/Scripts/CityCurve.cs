using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCurve : MonoBehaviour
{
    public Vector3 NodeA;
    public Vector3 NodeB;

    
    public List<Vector3> Samples()
    {
        List<Vector3> temp = new List<Vector3>();
       
            for (int j = 1; j < 31; j++)
            {
                temp.Add(Vector3.Lerp(NodeA, NodeB, j / 31));
            Debug.Log(NodeA);
            Debug.Log(NodeB);
            }

        return temp;

    }
}


