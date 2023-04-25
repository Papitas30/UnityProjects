using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrbanGenerator : MonoBehaviour
{
    public int maxDegreeCurve = 10;
    public Vector3 MapSize;
   public Bounds bounds;
    public int maxMainSegments =100000;
    public int maxMinorSegments=10000;
    public int roadLengthInMeters;
    public List<UrbanRoadSegment> MajorRoads = new List<UrbanRoadSegment>();
    public List<UrbanRoadSegment> MinorRoads = new List<UrbanRoadSegment>();
    public int PercentForkMainRoadChance = 10;
    public int Percent4WayMainRoadChance = 30;  

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void init()
    {
        transform.forward = Vector3.forward;
        MajorRoads.Clear();
        MinorRoads.Clear();
        int mainSegsLeft = maxMainSegments;
        int minorSegsLeft = maxMinorSegments;
        Vector3 Point = transform.position;
        bounds.center = Point;
        bounds.size = MapSize*2;
        Vector3 Forward = Vector3.forward;
        UrbanRoadSegment road1 = new UrbanRoadSegment();
        road1.startNode = Point;
        road1.endNode = Point + (Forward * roadLengthInMeters);
        mainSegsLeft--;
        UrbanRoadSegment LastRoad = road1;
        Vector3 OldForward = transform.forward;
        int multiMain = 1;
        int multiMainFork = 1;
        int AccumCurveMain = 0;
        int AccumCurveMainFork = 0;
        int j = 0;

        while (bounds.Contains(Point) && mainSegsLeft > 0)
        {
            if (Random.Range(0, 10) > 8)
            {
                if (Random.Range(0, 9) >2)
                {
                    multiMain *= -1;
                    AccumCurveMain = 0;
                }
                int curveAmount = Random.Range(0, maxDegreeCurve * multiMain);
                transform.rotation *= Quaternion.Euler(0, curveAmount, 0);
                Forward = transform.forward;
                OldForward = transform.forward;
                AccumCurveMain += curveAmount;

            }
            MajorRoads.Add(LastRoad);
            Point = LastRoad.endNode;
            UrbanRoadSegment theRoad = new UrbanRoadSegment();
            theRoad.startNode = Point;
            theRoad.endNode = Point + (Forward * roadLengthInMeters);

            mainSegsLeft--;
            if (Random.Range(1, 100) <= 100) 
                {
                    if (Random.Range(1, 100) < PercentForkMainRoadChance && j % 25 ==0)
                    {
                        Vector3 ForkForward = transform.forward;
                        int rand = Random.Range(0, 10);
                        if (rand > 5)
                        {
                            transform.rotation *= Quaternion.Euler(0, -90, 0);
                            ForkForward = transform.forward;

                        }
                        if (rand < 6)
                        {
                            transform.rotation *= Quaternion.Euler(0, 90, 0);
                            ForkForward = transform.forward;

                        }
                        Vector3 ForkPoint = LastRoad.endNode;
                        LastRoad.endNodeIsFork = true;
                        UrbanRoadSegment Forkroad1 = new UrbanRoadSegment();
                        Forkroad1.startNode = ForkPoint;
                        Forkroad1.endNode = ForkPoint + (ForkForward * roadLengthInMeters);
                        UrbanRoadSegment LastForkRoad = Forkroad1;
                        multiMainFork = 1;

                        mainSegsLeft--;

                        for (int i = 0; i < Random.Range((MapSize.x / roadLengthInMeters) - (MapSize.x / roadLengthInMeters) / 5, MapSize.x / roadLengthInMeters / 2); i++)
                        {
                            
                        
                        if (Random.Range(0, 10) > 8)
                            {
                            if (Random.Range(0, 9) > 2)
                            {
                                    multiMainFork *= -1;
                                    AccumCurveMainFork = 0;
                                }
                                int curveAmount = Random.Range(0, maxDegreeCurve * multiMainFork);
                                transform.rotation *= Quaternion.Euler(0, curveAmount, 0);
                                ForkForward = transform.forward;
                                AccumCurveMainFork += curveAmount;
                            }

                            MajorRoads.Add(LastForkRoad);
                            ForkPoint = LastForkRoad.endNode;
                            UrbanRoadSegment theForkRoad = new UrbanRoadSegment();
                            theForkRoad.startNode = ForkPoint;
                            theForkRoad.endNode = ForkPoint + (ForkForward * roadLengthInMeters);
                            LastForkRoad = theForkRoad;
                        
                            mainSegsLeft--;

                        }
                        MajorRoads.Add(LastForkRoad);
                        transform.forward = OldForward;

                    }
                }
            LastRoad = theRoad;
            j++;
            if (CheckDistanceFromOtherMainSegments(LastRoad.endNode, LastRoad.startNode) == true)
            {
                break;
            }
        }

      
        MajorRoads.Add(LastRoad);

     


    }

    private void OnDrawGizmos()
    {
        foreach(UrbanRoadSegment road in MajorRoads)
        {
           
                Gizmos.color = Color.red;

           
            Gizmos.DrawLine(road.startNode, road.endNode);
        }
        foreach (UrbanRoadSegment road in MinorRoads)
        {

            Gizmos.color = Color.blue;


            Gizmos.DrawLine(road.startNode, road.endNode);
        }
    }


    public bool CheckDistanceFromOtherMainSegments(Vector3 positionOfEndNode, Vector3 StartNode)
    {
        if (MajorRoads.Count - 4 > 0)
        {
            for (int i = 0; i < MajorRoads.Count - 4; i++)
            {
                for (int j = 0; j < roadLengthInMeters * 4; j++) 
                {
                    if (Vector3.Distance(positionOfEndNode, Vector3.Lerp(StartNode, positionOfEndNode, j/roadLengthInMeters*4)) < 7)
                    {
                        return true;
                    } 
                }
            }
        }
    
        return false;   
    
    }

    public bool CheckDistanceFromOtherMinorSegments(Vector3 positionOfEndNode)
    {
        if (MinorRoads.Count - 10 > 0)
        {
            for (int i = 0; i < MinorRoads.Count - 10; i++)
            {
                if (Vector3.Distance(positionOfEndNode, MinorRoads[i].endNode) < 8)
                {
                    return true;
                }
            }
        }

        return false;

    }



}
