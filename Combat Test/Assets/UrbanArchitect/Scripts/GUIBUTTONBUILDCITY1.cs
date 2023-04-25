using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UrbanMixerGenerator))]
public class GUIBUTTONBUILDCITY1 : Editor
{
    public override void OnInspectorGUI()
    {
        UrbanMixerGenerator CityGen = (UrbanMixerGenerator)target;
        DrawDefaultInspector();
 
        if (GUILayout.Button("Build Roads"))
        {
            CityGen.finished = false;
            CityGen.BuildACity();

        }
        if (GUILayout.Button("Allow Road Updates"))
        {
            RoadArchitect.RoadSystem RS = (RoadArchitect.RoadSystem)GameObject.FindObjectOfType(typeof(RoadArchitect.RoadSystem));
            RS.isAllowingRoadUpdates = true;

        }


        if (GUILayout.Button("Place Buildings"))
        {
            Selection.activeObject = GameObject.Find("Spline - Roads");

            CityGen.GenerateGOs();
            

        }

  




        if (GUILayout.Button("Clear Buildings"))
        {
            CityGen.ClearBuildings();

        }
      


    }
}
