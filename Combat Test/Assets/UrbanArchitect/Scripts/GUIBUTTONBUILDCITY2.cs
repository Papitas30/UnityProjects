using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadNetworkGenerator))]
public class GUIBUTTONBUILDCITY2 : Editor
{
    public override void OnInspectorGUI()
    {
        RoadNetworkGenerator CityGen = (RoadNetworkGenerator)target;
        DrawDefaultInspector();
 
        if (GUILayout.Button("Build Roads"))
        {
            CityGen.GenerateRoadData();

        }
        if (GUILayout.Button("Create Intersections"))
        {
            CityGen.createIntersections();

        }



    }
}
