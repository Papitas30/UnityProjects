using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using RoadArchitect.Roads;
using RoadArchitect;
[ExecuteAlways]
public class RoadNetworkGenerator : MonoBehaviour
{
    public int RoadLength;
    public int mapSize;
    private int b;
    public List<Vector3> NodesPoints = new List<Vector3>();
    public List<CityCurve> CurvesPoints = new List<CityCurve>();
    public float Angle;

    [HideInInspector]
    public Vector3 Size;

    // Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        Angle *= 180 / Mathf.PI;
    }

    public void GenerateRoadData()
    {
        b = 0;
        NodesPoints.Clear();
        CurvesPoints.Clear();

        EditorUtility.DisplayProgressBar("Gathering Road Data", " Part1of 5", 25);
        RoadArchitect.RoadSystem roadSystem = new GameObject("RS").AddComponent<RoadArchitect.RoadSystem>();

        for (int x = (int)transform.position.x; x < (int)transform.position.x + mapSize; x += RoadLength)
        {
            List<Vector3> pointsGenerated = new List<Vector3>();


            for (int z = (int)transform.position.z; z < (int)transform.position.z + mapSize; z += RoadLength)
            {
               pointsGenerated.Add((new Vector3(x, 0, z)));
               
            }
            RoadAutomation.CreateRoadProgrammatically(roadSystem, ref pointsGenerated);

        }

        for (int z = (int)transform.position.z; z < (int)transform.position.z + mapSize; z += RoadLength)
        {
            List<Vector3> pointsGenerated = new List<Vector3>();


            for (int x = (int)transform.position.x; x < (int)transform.position.x + mapSize; x += RoadLength)
            {
                
                    pointsGenerated.Add((new Vector3(x, 0, z)));
                

            }
            RoadAutomation.CreateRoadProgrammatically(roadSystem, ref pointsGenerated);

        }
        roadSystem.isMultithreaded = false;
        roadSystem.UpdateAllRoads();

        





        EditorUtility.ClearProgressBar();



    }
    public void createIntersections()
    {
        foreach (Road road in GameObject.FindObjectsOfType(typeof(Road)))
        {
            RoadAutomation.CreateIntersectionsProgrammaticallyForRoad(road, RoadIntersection.iStopTypeEnum.TrafficLight2);


        }
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




}
