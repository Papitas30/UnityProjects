using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine.TerrainUtils;
using System;

[ExecuteInEditMode]
public class Smoothit : MonoBehaviour
{
    public List<Terrain> terrains = new List<Terrain>();
    private List<float[,]> heights0 = new List<float[,]>();
    private List<float[,]> heightsEdited0 = new List<float[,]>();
    public bool getAllTerrains;
    [HideInInspector]
    public bool GrabTerrainHeights;
    [HideInInspector]
    public bool GrabEditsOnTerrain;
    [HideInInspector]
    public bool Smooth;
    [HideInInspector]
    public float sum;
    [HideInInspector]
    public int Div;
    [HideInInspector]
    public float BigSum;
    [HideInInspector]
    public int BigDiv;
    [HideInInspector]
    public float AvgHeight;
    public float iterations;

    private int i0 = 0;






    // Start is called before the first frame update
    void Start()
    {

    }
    private class TerrainNeighborInfo
    {
        public TerrainData terrainData;
        public Texture texture;
        public float offset;
    }






    public void grabAllTerrains()
    {
        EditorSceneManager.preventCrossSceneReferences = false;
        terrains.Clear();
        heights0.Clear();
        heightsEdited0.Clear();

        foreach (Terrain terrain in GameObject.FindObjectsOfType<Terrain>())
        {

            terrains.Add(terrain);
            GetHeightsOnTerrain();
        }
    }

    public static void startTheSmoothing(Terrain TheTerrain, int CenterX, int CenterY, int Radius, int iterations, float maxX, float MaxZ)
    {


        smoothAroundArea1(TheTerrain, CenterX, CenterY, Radius, iterations, maxX, MaxZ);



    }



    public static void smoothAroundArea1(Terrain TheTerrain, int CenterX, int CenterY, int Radius, int iterations, float maxX, float MaxZ)
    {
        float sum = 0;
        int Div = 0;
        float BigSum = 0;
        int BigDiv = 0;
        float[,] AllHeights = null;
        float[,] LerpHeights = null;
        AllHeights = TheTerrain.terrainData.GetHeights(0, 0, TheTerrain.terrainData.heightmapResolution, TheTerrain.terrainData.heightmapResolution);



        for (int i = -Radius; i <= Radius; i++)
        {

            for (int j = -Radius; j <= Radius; j++)
            {

                sum = 0;
                Div = 1;
                if (i + CenterX - 1 > 0 && j + CenterY - 1 > 0)
                {
                    if (i + CenterX - 1 < AllHeights.GetLength(0) && j + CenterY - 1 < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX - 1, j + CenterY - 1];
                        Div++;
                    }
                }
                if (i + CenterX - 1 > 0 && j + CenterY + 1 > 0 && j + CenterY + 1 < AllHeights.GetLength(1))
                {
                    if (i + CenterX - 1 < AllHeights.GetLength(0) && j + CenterY + 1 < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX - 1, j + CenterY + 1];
                        Div++;
                    }
                }
                if (i + CenterX + 1 > 0 && j + CenterY - 1 > 0 && i + CenterX + 1 < AllHeights.GetLength(0))
                {
                    if (i + CenterX + 1 < AllHeights.GetLength(0) && j + CenterY - 1 < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX + 1, j + CenterY - 1];
                        Div++;
                    }
                }
                if (i + CenterX + 1 > 0 && j + CenterY + 1 > 0 && i + CenterX + 1 < AllHeights.GetLength(0) && j + CenterY + 1 < AllHeights.GetLength(1))
                {
                    sum += AllHeights[i + CenterX + 1, j + CenterY + 1];
                    Div++;

                }
                if (i + CenterX > 0 && j + CenterY - 1 > 0)
                {
                    if (i + CenterX < AllHeights.GetLength(0) && j + CenterY - 1 < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX, j + CenterY - 1];
                        Div++;
                    }

                }

                if (i + CenterX - 1 > 0 && j + CenterY > 0)
                {
                    if (i + CenterX - 1 < AllHeights.GetLength(0) && j + CenterY < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX - 1, j + CenterY];
                        Div++;
                    }
                }

                if (i + CenterX > 0 && j + CenterY + 1 > 0 && j + CenterY + 1 < AllHeights.GetLength(1))
                {
                    if (i + CenterX < AllHeights.GetLength(0) && j + CenterY + 1 < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX, j + CenterY + 1];
                        Div++;
                    }
                }
                if (i + CenterX + 1 > 0 && j + CenterY > 0 && i + CenterX + 1 < AllHeights.GetLength(0))
                {
                    if (i + CenterX + 1 < AllHeights.GetLength(0) && j + CenterY < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX + 1, j + CenterY];
                        Div++;
                    }
                }
                if (i + CenterX > maxX && j + CenterY > MaxZ)
                {
                    if (i + CenterX < AllHeights.GetLength(0) && j + CenterY < AllHeights.GetLength(1))
                    {
                        sum += AllHeights[i + CenterX, j + CenterY];
                        AllHeights[i + CenterX, j + CenterY] = sum / Div;
                    }
                }


                BigSum += sum;
                BigDiv += Div;

            }
        }
        float AvgHeight = 0;
        AvgHeight = BigSum / BigDiv;
        BigDiv = 0;
        BigSum = 0;
        LerpHeights = TheTerrain.terrainData.GetHeights(0, 0, TheTerrain.terrainData.heightmapResolution, TheTerrain.terrainData.heightmapResolution);
        TheTerrain.terrainData.SetHeights(0, 0, AllHeights);

        for (int i = -Radius; i <= Radius; i++)
        {
            for (int j = -Radius; j <= Radius; j++)
            {
                AllHeights[i + CenterX, j + CenterY] = Mathf.LerpUnclamped(LerpHeights[i + CenterX, j + CenterY], AllHeights[i + CenterX, j + CenterY], 5.5f);
            }
        }

        TheTerrain.terrainData.SetHeights(0, 0, AllHeights);
    }






    public static float[,] RunUnityStyleTasksUnfinished(float[,] AllHeights, Vector3 Position, int i, int j)
    {
        RaycastHit hit;
        Physics.Raycast(new Vector3(Position.x + i, 6000, Position.z + j), Vector3.down, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Terrain"));
        Terrain TheTerrain = null;
        TheTerrain = hit.collider.GetComponent<Terrain>();
        if (AllHeights != null)
        {
            TheTerrain.terrainData.SetHeights(0, 0, AllHeights);
        }
        float Z = Position.x - hit.collider.GetComponent<Terrain>().transform.position.x;
        float X = Position.z - hit.collider.GetComponent<Terrain>().transform.position.z;
        if (X < 0)
        {
            X *= -1;
        }
        if (Z < 0)
        {
            Z *= -1;
        }


        return TheTerrain.terrainData.GetHeights(0, 0, TheTerrain.terrainData.heightmapResolution, TheTerrain.terrainData.heightmapResolution);


    }

    public static Terrain GetTerrain(Vector3 Position, int i, int j)
    {
        RaycastHit hit;
        Physics.Raycast(new Vector3(Position.x + i, 6000, Position.z + j), Vector3.down, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Terrain"));
        Terrain TheTerrain = null;
        TheTerrain = hit.collider.GetComponent<Terrain>();




        return TheTerrain;


    }
    public static void RunUnityStyleTasksfinished(float[,] allHeights, Terrain TheTerrain)
    {



        TheTerrain.terrainData.SetHeights(0, 0, allHeights);

    }
    // Update is called once per frame
    void Update()
    {
        if (terrains.Count > 0)
        {
            if (terrains[0] == null)
            {
                getAllTerrains = true;

            }
        }

        if (getAllTerrains)
        {
            getAllTerrains = false;
            grabAllTerrains();
        }

        if (GrabEditsOnTerrain)
        {
            GrabEditsOnTerrain = false;
            GetEditsOnTerrain();
        }
        if (GrabTerrainHeights)
        {
            GrabTerrainHeights = false;
            GetHeightsOnTerrain();
        }

        if (Smooth)
        {
            int i0 = 0;
            Smooth = false;


            FinallySmoothIt();
            // ApplyBlur(terrain);
            //i0++;


        }
        if (GameObject.FindObjectsOfType<Terrain>().Length != terrains.Count)
        {
            getAllTerrains = true;
        }



    }

    public void GetHeightsOnTerrain()
    {

        foreach (Terrain terrain in terrains)
        {

            heights0.Add(terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution));
        }
    }

    public void GetEditsOnTerrain()
    {
        int i = 0;
        heightsEdited0.Clear();
        foreach (Terrain terrain in terrains)
        {
            {


                heightsEdited0.Add(terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution));
                i++;
            }

        }
    }

    public void FinallySmoothIt()
    {
        float result = 0;
        int iii = 0;

       foreach(Terrain terrain in terrains) 
        { 

            if (heights0[iii] != heightsEdited0[iii])
            {


                for (int x = 0; x < iterations; x++)
                {
                    EditorUtility.DisplayProgressBar("Smoothing the Terrain", "Iteration " + x, x / iterations - 1);


                    for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
                    {
                        for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
                        {
                            if (heights0[iii][i, j] != heightsEdited0[iii][i, j] && i > 0 && i < terrain.terrainData.heightmapResolution && j > 0 && j < terrain.terrainData.heightmapResolution)
                            {
                                sum = 0;
                                Div = 1;
                                if (i - 1 >= 0 && j - 1 >= 0)
                                {

                                    sum += heightsEdited0[iii][i - 1, j - 1];
                                    Div++;

                                }
                                if (i - 1 >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                                {
                                    sum += heightsEdited0[iii][i - 1, j + 1];
                                    Div++;

                                }
                                if (i + 1 >= 0 && j - 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                                {

                                    sum += heightsEdited0[iii][i + 1, j - 1];
                                    Div++;

                                }
                                if (i + 1 >= 0 && j + 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution && j + 1 < terrain.terrainData.heightmapResolution)
                                {
                                    sum += heightsEdited0[iii][i + 1, j + 1];
                                    Div++;

                                }
                                if (i >= 0 && j - 1 >= 0)
                                {

                                    sum += heightsEdited0[iii][i, j - 1];
                                    Div++;

                                }

                                if (i - 1 >= 0 && j >= 0)
                                {

                                    sum += heightsEdited0[iii][i - 1, j];
                                    Div++;

                                }

                                if (i >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                                {

                                    sum += heightsEdited0[iii][i, j + 1];
                                    Div++;

                                }
                                if (i + 1 >= 0 && j >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                                {

                                    sum += heightsEdited0[iii][i + 1, j];
                                    Div++;

                                }
                                sum += heightsEdited0[iii][i, j];
                                float PreviousHeight = heightsEdited0[iii][i, j];
                                result = heightsEdited0[iii][i, j] = sum / Div;
                                BigSum += sum;
                                BigDiv += Div;
                            }

                        }
                    }
                }
                terrain.terrainData.SetHeights(0, 0, heightsEdited0[iii]);
                iii++;
            }
        }
        EditorUtility.ClearProgressBar();

    }


    public void FinallySmoothItAll()
    {
        float result = 0;
        int iii = 0;


        foreach (Terrain terrain in terrains)
        {



            for (int x = 0; x < iterations; x++)
            {
                EditorUtility.DisplayProgressBar("Smoothing the Terrain", "Iteration " + x, x / iterations - 1);


                for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
                {
                    for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
                    {
                        if (i > 0 && i < terrain.terrainData.heightmapResolution && j > 0 && j < terrain.terrainData.heightmapResolution)
                        {
                            sum = 0;
                            Div = 1;
                            if (i - 1 >= 0 && j - 1 >= 0)
                            {

                                sum += heightsEdited0[iii][i - 1, j - 1];
                                Div++;

                            }
                            if (i - 1 >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                            {
                                sum += heightsEdited0[iii][i - 1, j + 1];
                                Div++;

                            }
                            if (i + 1 >= 0 && j - 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                            {

                                sum += heightsEdited0[iii][i + 1, j - 1];
                                Div++;

                            }
                            if (i + 1 >= 0 && j + 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution && j + 1 < terrain.terrainData.heightmapResolution)
                            {
                                sum += heightsEdited0[iii][i + 1, j + 1];
                                Div++;

                            }
                            if (i >= 0 && j - 1 >= 0)
                            {

                                sum += heightsEdited0[iii][i, j - 1];
                                Div++;

                            }

                            if (i - 1 >= 0 && j >= 0)
                            {

                                sum += heightsEdited0[iii][i - 1, j];
                                Div++;

                            }

                            if (i >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                            {

                                sum += heightsEdited0[iii][i, j + 1];
                                Div++;

                            }
                            if (i + 1 >= 0 && j >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                            {

                                sum += heightsEdited0[iii][i + 1, j];
                                Div++;

                            }
                            sum += heightsEdited0[iii][i, j];
                            result = heightsEdited0[iii][i, j] = sum / Div;
                            BigSum += sum;
                            BigDiv += Div;
                        }

                    }
                }
            }
            terrain.terrainData.SetHeights(0, 0, heightsEdited0[iii]);
            iii++;

        }


        EditorUtility.ClearProgressBar();

    }

 



    public void FinallySmoothItQuick()
    {
        float result = 0;
        int iii = 0;
        
            foreach (Terrain terrain in terrains)
            {
                List<int> PosX = new List<int>();
                List<int> PosY = new List<int>();
                List<float> Sum2 = new List<float>();
                List<int> Div2 = new List<int>();
            int minI = 0;
            int maxI = 0;
            int minJ = 0;
            int maxJ = 0;
            float[,] heights = heightsEdited0[iii];
            if (heights0[iii] != heightsEdited0[iii])
            {
                for (int x = 0; x < 1; x++)
            {            EditorUtility.DisplayProgressBar("Scanning The Terrain", "Finding changes..." + x, x / iterations - 1);

               





                    for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
                    {
                        for (int j = 0; j < terrain.terrainData.heightmapResolution; j++)
                        {
                            if (heights0[iii][i, j] != heightsEdited0[iii][i, j] && i > 0 && i < terrain.terrainData.heightmapResolution && j > 0 && j < terrain.terrainData.heightmapResolution)
                            {
                                sum = 0;
                                Div = 1;
                                if (i - 1 >= 0 && j - 1 >= 0)
                                {

                                    sum += heightsEdited0[iii][i - 1, j - 1];
                                    Div++;

                                }
                                if (i - 1 >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                                {
                                    sum += heightsEdited0[iii][i - 1, j + 1];
                                    Div++;

                                }
                                if (i + 1 >= 0 && j - 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                                {

                                    sum += heightsEdited0[iii][i + 1, j - 1];
                                    Div++;

                                }
                                if (i + 1 >= 0 && j + 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution && j + 1 < terrain.terrainData.heightmapResolution)
                                {
                                    sum += heightsEdited0[iii][i + 1, j + 1];
                                    Div++;

                                }
                                if (i >= 0 && j - 1 >= 0)
                                {

                                    sum += heightsEdited0[iii][i, j - 1];
                                    Div++;

                                }

                                if (i - 1 >= 0 && j >= 0)
                                {

                                    sum += heightsEdited0[iii][i - 1, j];
                                    Div++;

                                }

                                if (i >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                                {

                                    sum += heightsEdited0[iii][i, j + 1];
                                    Div++;

                                }
                                if (i + 1 >= 0 && j >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                                {

                                    sum += heightsEdited0[iii][i + 1, j];
                                    Div++;

                                }
                                sum += heightsEdited0[iii][i, j];
                                float PreviousHeight = heightsEdited0[iii][i, j];
                                result = heights[i, j] = sum / Div;
                                Div2.Add(Div);
                                Sum2.Add(sum);
                                BigSum += sum;
                                BigDiv += Div;
                                PosX.Add(i);
                                PosY.Add(j);
                                minI = i;
                                minJ = j;
                                if(i > maxI)
                                {
                                    maxI = i;
                                }
                                if(j > maxJ)
                                {
                                    maxJ = j;
                                }
                            }

                        }
                    }
                }
                for (int x = 0; x < iterations; x++)
                {
                    EditorUtility.DisplayProgressBar("Smoothing The Terrain", "Iteration " + x, x / iterations - 1);







                    for (int i = minI-150; i < maxI+150; i++)
                    {
                        for (int j = minJ-150; j < maxJ+150; j++)
                        {
                            if (i > 0 && j > 0 && i < terrain.terrainData.heightmapResolution && j < terrain.terrainData.heightmapResolution)
                            {
                                if (heights0[iii][i, j] != heightsEdited0[iii][i, j] && i < terrain.terrainData.heightmapResolution - 10 && j < terrain.terrainData.heightmapResolution - 10)
                                {
                                    sum = 0;
                                    Div = 1;
                                    if (i - 1 >= 0 && j - 1 >= 0)
                                    {

                                        sum += heightsEdited0[iii][i - 1, j - 1];
                                        Div++;

                                    }
                                    if (i - 1 >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                                    {
                                        sum += heightsEdited0[iii][i - 1, j + 1];
                                        Div++;

                                    }
                                    if (i + 1 >= 0 && j - 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                                    {

                                        sum += heightsEdited0[iii][i + 1, j - 1];
                                        Div++;

                                    }
                                    if (i + 1 >= 0 && j + 1 >= 0 && i + 1 < terrain.terrainData.heightmapResolution && j + 1 < terrain.terrainData.heightmapResolution)
                                    {
                                        sum += heightsEdited0[iii][i + 1, j + 1];
                                        Div++;

                                    }
                                    if (i >= 0 && j - 1 >= 0)
                                    {

                                        sum += heightsEdited0[iii][i, j - 1];
                                        Div++;

                                    }

                                    if (i - 1 >= 0 && j >= 0)
                                    {

                                        sum += heightsEdited0[iii][i - 1, j];
                                        Div++;

                                    }

                                    if (i >= 0 && j + 1 >= 0 && j + 1 < terrain.terrainData.heightmapResolution)
                                    {

                                        sum += heightsEdited0[iii][i, j + 1];
                                        Div++;

                                    }
                                    if (i + 1 >= 0 && j >= 0 && i + 1 < terrain.terrainData.heightmapResolution)
                                    {

                                        sum += heightsEdited0[iii][i + 1, j];
                                        Div++;

                                    }
                                    sum += heightsEdited0[iii][i, j];
                                    float PreviousHeight = heightsEdited0[iii][i, j];
                                    result = heights[i, j] = sum / Div;
                                    Div2.Add(Div);
                                    Sum2.Add(sum);
                                    BigSum += sum;
                                    BigDiv += Div;
                                    PosX.Add(i);
                                    PosY.Add(j);

                                }
                            }
                        }
                    }
                }


                terrain.terrainData.SetHeights(0, 0, heights);
                iii++;
            }
        }

        EditorUtility.ClearProgressBar();

    }



}


        // OnGUI is called for rendering and handling GUI events

    

