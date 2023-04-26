using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadArchitect;
using RoadArchitect.Roads;
using System.Linq;

public class CurveTranslator : MonoBehaviour
{
    [HideInInspector]
    public UrbanSpline spline;
    [HideInInspector]
    public List<List<Vector3>> Roads = new List<List<Vector3>>();
    [HideInInspector]
    public CityCurve CurrentCurve;
    [HideInInspector]
    public int j = 0;

    public void AddRoads()
    {
        List<Vector3> points = new List<Vector3>(); 
        for(int i =0; i < spline.curves.Count;i++)
        {
    
            points.Add(spline.curves[i].NodeA);

            points.Add(spline.curves[i].NodeB);
            Roads.Add(points);  

        }

    }
    public CityCurve FindWhereBMatchesA(Vector3 NodeB, Vector3 NodeA)
    {
        foreach (CityCurve curve in spline.curves)
        {
            if (curve.NodeA == NodeB && Vector3.Distance(NodeA, curve.NodeB) < 135)
            {
                return curve;
            }
        }
        Debug.Log("Couldn't find Curve for NodeA");
        return null;    
    }
    public CityCurve FindWhereAMatchesB(Vector3 NodeA, Vector3 NodeB)
    {
        foreach (CityCurve curve in spline.curves)
        {
            if (curve.NodeA == NodeA && Vector3.Distance(NodeB, curve.NodeA) < 135)
            {
                return curve;
            }
        }
        Debug.Log("Couldn't find Curve for NodeB");

        return null;
    }

    public RoadSystem FindOrCreateRoadSystem()
    {
        RoadSystem roadSystem = null;
        if (GameObject.FindObjectOfType(typeof(RoadSystem)))
        {
            roadSystem = (RoadSystem)GameObject.FindObjectOfType(typeof(RoadSystem));   
        }
        else
        {
            roadSystem = new GameObject("RS").AddComponent<RoadSystem>();
        }
        return roadSystem;
    }
    public void createRoads()
    {
        Roads = Roads.Distinct().ToList();
        for (int i = 0; i < Roads.Count; i++)
        {
            List<Vector3> road = Roads[i];
            RoadAutomation.CreateRoadProgrammatically(FindOrCreateRoadSystem(), ref road);

        }

    }

}
