using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using RoadArchitect.Roads;
[ExecuteInEditMode]
public class UrbanMixerGenerator : MonoBehaviour
{


    #region Mostly Private Variables
    //Mostly Private Variables
    private UrbanSpline spline;
    private CityGenerator cityGenerator;
    [HideInInspector]
    public List<TerrainLayer> layers = new List<TerrainLayer>();
    private float multi = 1;
    [HideInInspector]
    private RaycastHit hit0;
    [HideInInspector]
    private List<Vector3> ThePoints2 = new List<Vector3>();
    private float LowestHeight;
    private float HighestHeight;
    private Vector3 half2;
    private List<GameObject> cleaupGOs = new List<GameObject>();
    private List<GameObject> BuildingGOs = new List<GameObject>();
    private RaycastHit hit00000;
    private float middleHeight;
    private System.Random rand;
    [HideInInspector]
    public List<Vector2> positionsOfNodes = new List<Vector2>();
    private List<Vector3> SpawnedGOs = new List<Vector3>();
    private RaycastHit hit1;
    private RaycastHit hit2;
    private Vector3 RoadsOffset;
    private float Angle;
    [HideInInspector]
    public List<Vector3> NormalListOfNodes = new List<Vector3>();
    private List<Vector3> ThePoints = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> CheckedPoints = new List<Vector3>();
    public int RoadAndBuildingsLayer;

    #endregion  //Mostly Private Variables

    #region public variables



   


    [Header("Buildings")]

    [Tooltip("Center Buildings are for the main part of your City. Think skyscrapers")]
    public GameObject[] Center;
    [Tooltip("How close together should the buildings be? A lower Number Means More Buildings If they Fit.")]
    public int CenterBuildingDensity;
    // public GameObject[] CenterPoles;
    //[Tooltip("How close together should the poles be? A Higher Number Means More poles If they Fit.")]
    // public int CenterPoleDensity;
    // public GameObject[] CenterSigns;
    // [Tooltip("How close together should the Signs be? A Higher Number Means More Signs If they Fit.")]
    //public int CenterSignDensity;
    // public GameObject[] CenterProps;
    // [Tooltip("How close together should the Props be? A Higher Number Means More Props If they Fit.")]
    // public int CenterPropDensity;
    [Tooltip("Middle Buildings are for the shops and stuff like that")]
    public GameObject[] Middle;
    [Tooltip("How close together should the buildings be? A lower Number Means More Buildings If they Fit.")]
    public int MiddleBuildingDensity;
    //public GameObject[] MiddlePoles;
    // [Tooltip("How close together should the poles be? A Higher Number Means More poles If they Fit.")]
    //public int MiddlePoleDensity;
    // public GameObject[] MiddleSigns;
    //[Tooltip("How close together should the Signs be? A Higher Number Means More Signs If they Fit.")]
    // public int MiddleSignDensity;
    // public GameObject[] MiddleProps;
    // [Tooltip("How close together should the Props be? A Higher Number Means More Props If they Fit.")]
    // public int MiddlePropDensity;
    [Tooltip("OuterMiddle Buildings are for the lower class houses")]
    public GameObject[] OuterMiddle;
    [Tooltip("How close together should the buildings be? A lower Number Means More Buildings If they Fit.")]
    public int OuterMiddleBuildingDensity;
    // public GameObject[] OuterMiddlePoles;
    //[Tooltip("How close together should the poles be? A Higher Number Means More poles If they Fit.")]
    // public int OuterMiddlePoleDensity;
    // public GameObject[] OuterMiddleSigns;
    //[Tooltip("How close together should the Signs be? A Higher Number Means More Signs If they Fit.")]
    // public int OuterMiddleSignDensity;
    // public GameObject[] OuterMiddleProps;
    //[Tooltip("How close together should the Props be? A Higher Number Means More Props If they Fit.")]
    // public int OuterMiddlePropDensity;
    [Tooltip("Suburban Buildings are for the upper class houses")]
    public GameObject[] Suburban;
    [Tooltip("How close together should the buildings be? A lower Number Means More Buildings If they Fit.")]
    public int SuburbanBuildingDensity;
    // public GameObject[] SuburbanPoles;
    // [Tooltip("How close together should the poles be? A Higher Number Means More poles If they Fit.")]
    // public int SuburbanPoleDensity;
    // public GameObject[] SuburbanSigns;
    // [Tooltip("How close together should the Signs be? A Higher Number Means More Signs If they Fit.")]
    // public int SuburbanSignDensity;
    //  public GameObject[] SuburbanProps;
    //  [Tooltip("How close together should the Props be? A Higher Number Means More Props If they Fit.")]
    //  public int SuburbanPropDensity;




    [Header("City Size")]
    [Tooltip("Set The Size of your City")]

    public int mapSize;
    [Header("Sea level")]
    [Tooltip("Set The Sea Level")]
    public float Sealevel = -6;
   
    [Header("Extra Settings")]
    [Tooltip("Setting this to true will enable pathfinding")]
    [HideInInspector]
    public bool PathFindAroundSteepAreas = false;
    [Tooltip("Adjusting this value changes the size of all districts")]
    public float districtMultiplier;
    [HideInInspector]
    public bool finished;









    #endregion

    #region Necessary Methods





    public void ClearBuildings()
    {
        foreach (BuildingType go in GameObject.FindObjectsOfType(typeof(BuildingType)))
        {
            DestroyImmediate(go.gameObject);
        }


    }


    public void BuildACity()//Starts The City Building Process
    {


        if (!Application.isPlaying)
        {
            CheckedPoints.Clear();
            cityGenerator = GetComponent<CityGenerator>();

            GameObject tempSpline = new GameObject("Roads");
           tempSpline.AddComponent<UrbanSpline>();
            spline = tempSpline.GetComponent<UrbanSpline>();    
            LowestHeight = GetLowestHeightFromSamples();
            HighestHeight = GetHighestHeightFromSamples();
            middleHeight = (LowestHeight + HighestHeight) / 2;
            positionsOfNodes.Clear();
            NormalListOfNodes.Clear();
            SpawnedGOs.Clear();
            ThePoints.Clear();
            ThePoints2.Clear();
            if (GameObject.Find("Water Surface") != null)
            {
                Sealevel = GameObject.Find("Water Surface").transform.position.y + 3;
            }

            Physics.Raycast(transform.position + new Vector3(0, 11000, 0), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer);



            ThreadProc();





        }

    }


    private void ThreadProc()
    {


        transform.GetComponent<CityGenerator>().mapSize = mapSize;

        transform.GetComponent<CityGenerator>().BuildCity();



    } //Builds The Initial Map


    public UrbanMixerRoad findRoad(Vector3 position, Vector3 direction)
    {
        RaycastHit[] hits = Physics.RaycastAll(position+(direction*32)+(Vector3.up*3000), Vector3.down, Mathf.Infinity, RoadAndBuildingsLayer);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<UrbanMixerRoad>())
            {
                return hit.collider.GetComponent<UrbanMixerRoad>();
            }
        }
        CheckedPoints.Add(position + (direction * 32));
        return null;
    }

    private float GetHighestHeightFromSamples()
    {
        RaycastHit Highest;
        float high = 0;
        high = 0;

        //Get Regular Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * 176 / 2, 6000, mapSize * 176 / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * 176 / 2, 6000, mapSize * -176 / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * -176 / 2, 6000, mapSize * -176 / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * -176 / 2, 6000, mapSize * 176 / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(0, 6000, 0), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }

        //Do Random Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        //Do Random Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        //Do Random Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        //Do Random Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
            {
                high = Highest.point.y;
            }
        }
        return high;
    }

    private float GetHighestBuildingBaseHeightFromSamples(Vector3 position, float sizeZ, Vector3 right, Vector3 forward)
    {
        RaycastHit Highest;
        float high = 0;
        high = 0;
        for (int x = 0; x < sizeZ; x++)
        {
            for (int y = 0; y < sizeZ; y++)
            {
                if ((Physics.Raycast(new Vector3(position.x - sizeZ / 2, 0, position.z - sizeZ / 2) + new Vector3(x, 6000, y), Vector3.down, out Highest, Mathf.Infinity, RoadAndBuildingsLayer)))
                {
                    if (Highest.point.y > high && Highest.point.y > Sealevel + 3)
                    {
                        high = Highest.point.y;
                    }

                }
            }
        }





        return high;
    }//Finds A good Spot for the Building To sit.

    private float GetLowestHeightFromSamples()
    {
        RaycastHit lowest;
        float low = 0;
        low = Mathf.Infinity;

        //Get Regular Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * 176 / 2, 6000, mapSize * 176 / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * 176 / 2, 6000, mapSize * -176 / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * -176 / 2, 6000, mapSize * -176 / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * -176 / 2, 6000, mapSize * 176 / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(0, 6000, 0), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }
        }

        //Do Random Samples
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }
        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(mapSize * Random.Range(-176, 176) / 2, 6000, mapSize * Random.Range(-176, 176) / 2), Vector3.down, out lowest, Mathf.Infinity, RoadAndBuildingsLayer))
        {
            if (lowest.point.y < low && lowest.point.y > Sealevel + 3)
            {
                low = lowest.point.y;
            }

        }
        return low;
    }

    public float getSoftAngle(Vector3 position)
    {
        RaycastHit hit0001 = new RaycastHit();

        Physics.Raycast(position + new Vector3(0, 11000, 0), Vector3.down, out hit0001, Mathf.Infinity, RoadAndBuildingsLayer);

        float angle1 = Vector3.Angle(hit0001.normal, Vector3.up);

        float AVG = angle1 - 4;

        return AVG;




    }

    public List<RaycastHit> HitsOnlyRoad(Vector3 position, Vector3 direction, Vector3 size)
    {

        List<Collider> result = new List<Collider>();
        List<RaycastHit> results = new List<RaycastHit>();

        List<RaycastHit> hits = Physics.BoxCastAll(position +(direction*32) + new Vector3(0,11000,0), new Vector3(size.x / 2, size.y/2, size.z / 2), Vector3.down).ToList();


        foreach (RaycastHit hit in hits)
        {
            Collider collider = hit.collider;

            if (result.Contains(collider))
                continue;


            // Skip Terrains
            Terrain terrain = collider.GetComponent<Terrain>();
            if (terrain != null)
                continue;
            // If hit road
           UrbanMixerRoad Road = collider.GetComponent<UrbanMixerRoad>();

            if (Road != null)
            {
                result.Add(Road.transform.GetComponent<Collider>());
                results.Add(hit);
                return results;
            }







        }
        return results;

    }


    public List<Collider> colliders(Vector3 position, Vector3 size, Quaternion Rotation)
    {

        List<Collider> result = new List<Collider>();

        RaycastHit[] hits = Physics.BoxCastAll(position, ((size)) / 1.55f, Vector3.down, Rotation);


        foreach (RaycastHit hit in hits)
        {
            Collider collider = hit.collider;

            if (result.Contains(collider))
                continue;


            // Skip Terrains
            Terrain terrain = collider.GetComponent<Terrain>();
            if (terrain != null)
                continue;
            // If hit road
            UrbanMixerRoad Road = collider.GetComponent<UrbanMixerRoad>();

            if (Road != null)
            {
                result.Add(Road.transform.GetComponent<Collider>());
                continue;
            }



            result.Add(hit.collider);



        }
        return result;

    }
    public List<Collider> collidersNonRoad(Vector3 position, Vector3 size)
    {

        List<Collider> result = new List<Collider>();

        RaycastHit[] hits = Physics.BoxCastAll(position, size / 2, Vector3.down);


        foreach (RaycastHit hit in hits)
        {
            Collider collider = hit.collider;

            if (result.Contains(collider))
                continue;


            // Skip Terrains
            Terrain terrain = collider.GetComponent<Terrain>();
            if (terrain != null)
                continue;
            // If hit road
            UrbanMixerRoad Road = collider.GetComponent<UrbanMixerRoad>();

            if (Road != null)
            {
                continue;
            }



            result.Add(hit.collider);




        }
        return result;

    }

    public void GenerateGOs()
    {
        ClearBuildings();
        DestroyImmediate(GameObject.Find("Restraightner"));
        layers.Clear();













        GameObject gameObject2 = new GameObject("Restraightner");

        gameObject2.AddComponent<ReStraightener>();
        Collider[] array = null;

        int q = 0;
        int num = 0;
        Vector3 vector = Vector3.zero;
        _ = Vector3.zero;
        Vector3 vector2 = Vector3.zero;
        Vector3 zero = Vector3.zero;
        Vector3 vector3 = Vector3.zero;
        float num2 = 0;
        GameObject gameObject3 = null;
        GameObject gameObject4 = null;
        List<CityCurve> curves = new List<CityCurve>();
        float num3 = 0f;
        int jDensity = 0;
        int WhatDistrict = 0;





        foreach (CityCurve curf in spline.curves)
        {
            curves.Clear();
            curves.Add(curf);
            GameObject LastBuilding = null;
            GameObject LastBuilding2 = null;
            Bounds RoadBounds = new Bounds();
            RoadBounds.center = curf.NodeA;
            RoadBounds.size = new Vector3(45, 80, 45);
            Bounds RoadBounds2 = new Bounds();
            RoadBounds.center = curf.NodeA;
            RoadBounds.size = new Vector3(45, 80, 45);

           

            
                jDensity = CenterBuildingDensity;
                num = jDensity;
                num3 = 1f / jDensity;
          
            for (int j = 1; j < jDensity; j++)
            {





                if (Vector3.Distance(curf.NodeA, Vector3.zero) < 1000f * districtMultiplier)
                {


                    gameObject3 = Center[Random.Range(0, Center.Length)];
                    if (gameObject3 == LastBuilding)
                    {
                        while (gameObject3 == LastBuilding)
                        {
                            gameObject3 = Center[Random.Range(0, Center.Length)];

                        }
                    }
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    num2 = 24;
                    jDensity = CenterBuildingDensity;

                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                if (Vector3.Distance(curf.NodeA, Vector3.zero) < 1500f * districtMultiplier && Vector3.Distance(curf.NodeA, Vector3.zero) >= 1000f * districtMultiplier)
                {
                    gameObject3 = Middle[Random.Range(0, Middle.Length)];
                    if (gameObject3 == LastBuilding)
                    {
                        while (gameObject3 == LastBuilding)
                        {
                            gameObject3 = Middle[Random.Range(0, Middle.Length)];

                        }
                    }
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    jDensity = MiddleBuildingDensity;

                    num2 = 24;
                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                if (Vector3.Distance(curf.NodeA, Vector3.zero) < 2000f * districtMultiplier && Vector3.Distance(curf.NodeA, Vector3.zero) >= 1500f * districtMultiplier)
                {
                    gameObject3 = OuterMiddle[Random.Range(0, OuterMiddle.Length)];
                    if (gameObject3 == LastBuilding)
                    {
                        while (gameObject3 == LastBuilding)
                        {
                            gameObject3 = OuterMiddle[Random.Range(0, OuterMiddle.Length)];

                        }
                    }
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    jDensity = OuterMiddleBuildingDensity;

                    num2 = 24;
                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                if (Vector3.Distance(curf.NodeA, Vector3.zero) > 2000f * districtMultiplier)
                {
                    gameObject3 = Suburban[Random.Range(0, Suburban.Length)];
                    if (gameObject3 == LastBuilding)
                    {
                        while (gameObject3 == LastBuilding)
                        {
                            gameObject3 = Suburban[Random.Range(0, Suburban.Length)];

                        }
                    }
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    jDensity = SuburbanBuildingDensity;

                    num2 = 24;
                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                Vector3 vector4 = Vector3.Lerp(curf.NodeA, curf.NodeB, (float)j * num3);
                vector2 = Vector3.Lerp(curf.NodeA, curf.NodeB, (float)j - 1 * num3);
                zero = (vector4 - vector2).normalized;
                vector3 = Vector3.Cross(Vector3.up, zero);
                Vector3 nextPos = Vector3.Lerp(curf.NodeA, curf.NodeB, (float)j * num3);
                Vector3 PrevPos = Vector3.Lerp(curf.NodeA, curf.NodeB, (float)j - 1 * num3);
                Vector3 forward = (nextPos - PrevPos).normalized;
                Vector3 right = Vector3.Cross(Vector3.up, forward);
                Vector3 location = vector4 + (right * gameObject3.GetComponent<BuildingType>().distanceFromRoad);
                Vector3 Rlocation = vector4;
                Vector3 LastLocation = Vector3.zero;
                Vector3 CurrentLocation = Vector3.zero;
                Vector3 StopLocation = location - (right * 11);
                if (gameObject3.GetComponent<BuildingType>().distanceFromRoad > 11)
                {
                    float point = gameObject3.GetComponent<BuildingType>().distanceFromRoad - 11;
                    StopLocation = location - (right * (11 + point) * 0.52f);
                }






                array = Physics.OverlapBox(location, (vector + new Vector3(0f, 5f, 0f)) * 0.5f);
                if (Physics.Raycast(location + new Vector3(0f, 6000f, 0f), Vector3.down, out hit1, float.PositiveInfinity))
                {
                    transform.position = location;
                    transform.LookAt(Rlocation);
                    transform.transform.rotation *= Quaternion.Euler(0f, gameObject3.GetComponent<BuildingType>().YRotationOffset, 0f);


                    if ((colliders(location, gameObject3.GetComponent<BuildingType>().size, transform.rotation).Count == 0) && !RoadBounds.Contains(location) && !RoadBounds2.Contains(location))
                    {
                        if (location.x != 0 && location.z != 0)
                        {
                        
                            LastBuilding = gameObject3;
                            gameObject4 = Object.Instantiate(gameObject3, location, Quaternion.identity, gameObject2.transform);
                            gameObject4.GetComponent<BuildingType>().heightOfRoad = Rlocation.y;
                            gameObject4.transform.LookAt(Rlocation);
                            gameObject4.transform.rotation *= Quaternion.Euler(0f, gameObject4.GetComponent<BuildingType>().YRotationOffset, 0f);
                            q++;
                        }
                    }
                }
                transform.transform.rotation = Quaternion.Euler(0f, 0, 0f);

                if (Vector3.Distance(curf.NodeA, Vector3.zero) < 1000f * districtMultiplier)
                {

                    WhatDistrict = 0;

                    gameObject3 = Center[Random.Range(0, Center.Length)];
                    if (gameObject3 == LastBuilding2)
                    {
                        while (gameObject3 == LastBuilding2)
                        {
                            gameObject3 = Center[Random.Range(0, Center.Length)];

                        }
                    }
                    num2 = 24;
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    jDensity = CenterBuildingDensity;

                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                if (Vector3.Distance(curf.NodeA, Vector3.zero) < 1500f * districtMultiplier && Vector3.Distance(curf.NodeA, Vector3.zero) >= 1000f * districtMultiplier)
                {
                    WhatDistrict = 1;
                    gameObject3 = Middle[Random.Range(0, Middle.Length)];
                    if (gameObject3 == LastBuilding2)
                    {
                        while (gameObject3 == LastBuilding2)
                        {
                            gameObject3 = Middle[Random.Range(0, Middle.Length)];

                        }
                    }
                    jDensity = MiddleBuildingDensity;
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    num2 = 24;
                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                if (Vector3.Distance(curf.NodeA, Vector3.zero) < 2000f * districtMultiplier && Vector3.Distance(curf.NodeA, Vector3.zero) >= 1500f * districtMultiplier)
                {
                    WhatDistrict = 2;
                    gameObject3 = OuterMiddle[Random.Range(0, OuterMiddle.Length)];
                    if (gameObject3 == LastBuilding2)
                    {
                        while (gameObject3 == LastBuilding2)
                        {
                            gameObject3 = OuterMiddle[Random.Range(0, OuterMiddle.Length)];

                        }
                    }
                    jDensity = OuterMiddleBuildingDensity;
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    num2 = 24;
                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                if (Vector3.Distance(curf.NodeA, Vector3.zero) > 2000f * districtMultiplier)
                {
                    WhatDistrict = 3;
                    gameObject3 = Suburban[Random.Range(0, Suburban.Length)];
                    if (gameObject3 == LastBuilding2)
                    {
                        while (gameObject3 == LastBuilding2)
                        {
                            gameObject3 = Suburban[Random.Range(0, Suburban.Length)];

                        }
                    }
                    jDensity = SuburbanBuildingDensity;
                    if (gameObject3.GetComponent<BuildingType>() == null)
                    {
                        gameObject3.AddComponent<BuildingType>();
                        gameObject3.GetComponent<BuildingType>().size = new Vector3(0, 120, 0);
                        float sizeX = 0;
                        float sizeZ = 0;
                        if (gameObject3.GetComponent<MeshFilter>() != null)
                        {
                            if (gameObject3.GetComponent<MeshCollider>() == null)
                            {
                                gameObject3.gameObject.AddComponent<MeshCollider>();
                                gameObject3.GetComponent<MeshCollider>().sharedMesh = gameObject3.GetComponent<MeshFilter>().sharedMesh;
                            }
                            gameObject3.GetComponent<BuildingType>().size = new Vector3(gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                            sizeX = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                            sizeZ = gameObject3.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;
                        }


                        foreach (Transform trans in gameObject3.transform)
                        {

                            if (trans.GetComponent<MeshFilter>() != null)
                            {
                                if (trans.GetComponent<MeshCollider>() == null)
                                {
                                    trans.gameObject.AddComponent<MeshCollider>();
                                    trans.GetComponent<MeshCollider>().sharedMesh = trans.GetComponent<MeshFilter>().sharedMesh;
                                }
                                if (trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x > sizeX && trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z > sizeZ)
                                {
                                    gameObject3.GetComponent<BuildingType>().size = new Vector3(trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x, 120, trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z);
                                    sizeX = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
                                    sizeZ = trans.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

                                }
                            }

                        }
                        gameObject3.GetComponent<BuildingType>().distanceFromRoad = 11 + 7 + gameObject3.GetComponent<BuildingType>().size.z;

                    }
                    num2 = 24;
                    vector = new Vector3((int)gameObject3.GetComponent<BuildingType>().size.x, 0.01f, (int)gameObject3.GetComponent<BuildingType>().size.z);
                }
                nextPos = Vector3.Lerp(curf.NodeA, curf.NodeB, (float)j * num3);
                PrevPos = Vector3.Lerp(curf.NodeA, curf.NodeB, (float)j - 1 * num3);
                forward = (nextPos - PrevPos).normalized;
                right = Vector3.Cross(Vector3.up, forward);
                location = vector4 - (right * gameObject3.GetComponent<BuildingType>().distanceFromRoad);
                Rlocation = vector4;
                LastLocation = Vector3.zero;
                CurrentLocation = Vector3.zero;
                StopLocation = location + (right * 11);
                if (gameObject3.GetComponent<BuildingType>().distanceFromRoad > 11)
                {
                    float point = gameObject3.GetComponent<BuildingType>().distanceFromRoad - 11;
                    StopLocation = location + (right * (11 + point) * 0.52f);
                }






                array = Physics.OverlapBox(location, (vector + new Vector3(0f, 5f, 0f)) * 0.5f);
                if (Physics.Raycast(location + new Vector3(0f, 6000f, 0f), Vector3.down, out hit1, float.PositiveInfinity))
                {
                    transform.position = location;
                    transform.LookAt(Rlocation);
                    transform.transform.rotation *= Quaternion.Euler(0f, gameObject3.GetComponent<BuildingType>().YRotationOffset, 0f);

                    if ((colliders(location, gameObject3.GetComponent<BuildingType>().size, transform.rotation).Count == 0) && !RoadBounds.Contains(location) && !RoadBounds2.Contains(location))
                    {
                        if (location.x != 0 && location.z != 0)
                        {

                            
                            LastBuilding2 = gameObject3;
                            gameObject4 = Object.Instantiate(gameObject3, location, Quaternion.identity, gameObject2.transform);
                            gameObject4.GetComponent<BuildingType>().heightOfRoad = Rlocation.y;
                            gameObject4.transform.LookAt(Rlocation);
                            gameObject4.transform.rotation *= Quaternion.Euler(0f, gameObject4.GetComponent<BuildingType>().YRotationOffset, 0f);
                            q++;

                        }
                    }
                }
            }

            EditorUtility.DisplayProgressBar("Spawning Buildings", q + " Buildings Spawned", q / 1200);
        }

        transform.position = Vector3.zero;
        EditorUtility.ClearProgressBar();
        transform.transform.rotation = Quaternion.Euler(0f, 0, 0f);


    }









    public void GetNewHeightForBuilding()
    {






       
















        foreach (BuildingType building in GameObject.FindObjectsOfType(typeof(BuildingType)))
        {
            building.transform.position = new Vector3(building.transform.position.x, GetHighestBuildingBaseHeightFromSamples(building.transform.position, building.size.z, building.transform.right, building.transform.forward), building.transform.position.z);
        }



       


    }








    private Vector3 LookUpGSDNodeMajor(Vector3 position)//Looks Up a Node
    {
        foreach (Vector3 node in NormalListOfNodes)
        {
            if (Vector3.Distance(new Vector3(node.x, 0, node.z), new Vector3(position.x, 0, position.z)) < 35 *multi)
            {
                return node;
            }


        }
        return Vector3.zero;

    }




    #endregion

    void Update()
    {

   



    }

    public void GenerateTheRoads()
    {
        multi = 1;

        
        {
            for (int i = 0; i < transform.GetComponent<CityGenerator>().roadGraph.MajorNodes.Count; i++)
            {

                if (!positionsOfNodes.Contains(new Vector2(transform.GetComponent<CityGenerator>().roadGraph.MajorNodes[i].X, transform.GetComponent<CityGenerator>().roadGraph.MajorNodes[i].Y)))
                {
                    positionsOfNodes.Add(new Vector2(transform.GetComponent<CityGenerator>().roadGraph.MajorNodes[i].X, transform.GetComponent<CityGenerator>().roadGraph.MajorNodes[i].Y));
                }
            }

            for (int i = 0; i < transform.GetComponent<CityGenerator>().roadGraph.MinorNodes.Count; i++)
            {
                if (!positionsOfNodes.Contains(new Vector2(transform.GetComponent<CityGenerator>().roadGraph.MinorNodes[i].X, transform.GetComponent<CityGenerator>().roadGraph.MinorNodes[i].Y)))
                {
                    positionsOfNodes.Add(new Vector2(transform.GetComponent<CityGenerator>().roadGraph.MinorNodes[i].X, transform.GetComponent<CityGenerator>().roadGraph.MinorNodes[i].Y));
                }
            }


            for (int i = 0; i < positionsOfNodes.Count; i++)
            {



                if (Physics.Raycast(RoadsOffset + transform.position + new Vector3(positionsOfNodes[i].x * (12 * multi), 11000, positionsOfNodes[i].y * (12 * multi)), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer))
                {

                    if (hit1.point.y > Sealevel + 3 && Vector3.Angle(hit1.normal, Vector3.up) ==0)
                    {


                        NormalListOfNodes.Add(new Vector3(hit1.point.x, hit1.point.y, hit1.point.z));




                    }



                }

            }


            

            EditorUtility.ClearProgressBar();
          



            foreach (GraphModel.Edge edge in transform.GetComponent<CityGenerator>().roadGraph.MajorEdges)
            {








                Vector3 NodeA = Vector3.zero;
                Vector3 NodeB = Vector3.zero;


                Physics.Raycast(new Vector3(transform.position.x, 6000, transform.position.z), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer);



                NodeA = LookUpGSDNodeMajor(RoadsOffset + new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(edge.NodeA.X * 12, 0, edge.NodeA.Y * 12));






                Physics.Raycast(new Vector3(transform.position.x, 6000, transform.position.z), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer);


                NodeB = LookUpGSDNodeMajor(RoadsOffset + new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(edge.NodeB.X * 12, 0, edge.NodeB.Y * 12));



                if (NodeA != Vector3.zero && NodeB != Vector3.zero)
                {



                    CityCurve curve = new GameObject("curve").AddComponent<CityCurve>();
                    curve.NodeA = NodeA;    
                    curve.NodeB = NodeB;    


                    spline.curves.Add(curve);
                    curve.transform.parent = spline.transform;
                    Vector3 forward = (NodeB - NodeA).normalized;


                    spline.curves.Add(curve);
                    curve.transform.parent = spline.transform;
                    BuildRoadsOnTerrain(NodeA + (-forward * 10), NodeB + (forward * 10));







                }





            }






            foreach (GraphModel.Edge edge in transform.GetComponent<CityGenerator>().roadGraph.MinorEdges)
            {

                Vector3 NodeA = Vector3.zero;
                Vector3 NodeB = Vector3.zero;


                Physics.Raycast(new Vector3(transform.position.x, 6000, transform.position.z), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer);



                NodeA = LookUpGSDNodeMajor(RoadsOffset + new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(edge.NodeA.X * 12, 0, edge.NodeA.Y * 12));






                Physics.Raycast(new Vector3(transform.position.x, 6000, transform.position.z), Vector3.down, out hit1, Mathf.Infinity, RoadAndBuildingsLayer);


                NodeB = LookUpGSDNodeMajor(RoadsOffset + new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(edge.NodeB.X * 12, 0, edge.NodeB.Y * 12));



                if (NodeA != Vector3.zero && NodeB != Vector3.zero)
                {



                    CityCurve curve = new GameObject("curve").AddComponent<CityCurve>();
                    curve.NodeA = NodeA;
                    curve.NodeB = NodeB;
                    Vector3 forward = (NodeB - NodeA).normalized;


                    spline.curves.Add(curve);
                    curve.transform.parent = spline.transform;
                    BuildRoadsOnTerrain(NodeA+(-forward*10), NodeB + (forward * 10));







                }






            }

            











          


            EditorUtility.ClearProgressBar();




            RoadArchitect.RoadSystem RS = (RoadArchitect.RoadSystem)GameObject.FindObjectOfType(typeof(RoadArchitect.RoadSystem));




        }

    }

    public void BuildRoadsOnTerrain(Vector3 NodeA, Vector3 NodeB)
    {
        CurveTranslator curveTranslator = GetComponent<CurveTranslator>();
        RoadArchitect.RoadSystem RS = curveTranslator.FindOrCreateRoadSystem();
        RS.isAllowingRoadUpdates = false;
        List<Vector3> road = new List<Vector3>();

        road.Add(snapToGround(NodeA)+ new Vector3(0, 0.24f, 0));    
        road.Add(snapToGround(NodeB) + new Vector3(0, 0.24f, 0));
      RoadArchitect.Road road1 = RoadAutomation.CreateRoadProgrammatically(RS, ref road);
        road1.isHeightModificationEnabled = false;



    }


    public CityCurve WhichCurveMatchesThatRoadSegment(RoadGeneration.RoadSegment roadSegment, UrbanSpline spline)
    {
        foreach(CityCurve curve in spline.curves)
        {
            if(Vector3.Distance(new Vector3(roadSegment.NodeFrom.X, 0, roadSegment.NodeFrom.Y), curve.NodeA/12) <10 && Vector3.Distance(new Vector3(roadSegment.NodeTo.X, 0, roadSegment.NodeTo.Y), curve.NodeB / 12) < 10)
            {
                return curve;
            }
        }
        Debug.Log("Returned Null");
        return null;
    }

    public Vector3 CheckIfNodeExists(UrbanSpline Spline, Vector3 position)
    {
        foreach (CityCurve curve in Spline.curves)
        {
            if (curve.NodeA == position)
            {
                return curve.NodeA;
            }
            if (curve.NodeB == position)
            {
                return curve.NodeB;
            }
        }
        return Vector3.zero;
    }

  public Vector3 snapToGround(Vector3 point)
    {
        RaycastHit hit;
        Physics.Raycast(point + (Vector3.up * 1000), Vector3.down, out hit, Mathf.Infinity, RoadAndBuildingsLayer);
        return hit.point;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;



        Gizmos.DrawWireCube(new Vector3(transform.position.x, hit0.point.y, transform.position.z), new Vector3(4096 / 174 * mapSize, 4096 / 174 * mapSize, 4096 / 174 * mapSize));
        Gizmos.color = Color.blue;
        foreach (Vector3 point in CheckedPoints)
        {
            Gizmos.DrawCube(point, Vector3.one);
        }
    }
}


