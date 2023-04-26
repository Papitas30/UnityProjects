using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Smoothit))]
public class GUIButtonSmoothIt : Editor
{
    public override void OnInspectorGUI()
    {
        Smoothit Smooth = (Smoothit)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Smooth Changes In Terrain"))
        {
            Smooth.GetEditsOnTerrain();
            Smooth.FinallySmoothItQuick();

        }
        if (GUILayout.Button("Smooth Changes In Terrain (old)"))
        {
            Smooth.GetEditsOnTerrain();
            Smooth.FinallySmoothItAll();

        }

 

    }
}