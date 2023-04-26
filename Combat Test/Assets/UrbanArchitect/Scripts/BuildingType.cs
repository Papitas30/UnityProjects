using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class BuildingType : MonoBehaviour
{
   
    public Vector3 size;
    [Range(0, 100)]
    public float distanceFromRoad;
    public float ID;
    [Range(0,360)]
    public float YRotationOffset;
    [HideInInspector]
    public float heightOfRoad;

    private void Start()
    {
        ID = Random.Range(1, 10000000);


    }

    private void Update()
    {
        
        

        
    
    if(gameObject.GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
            gameObject.GetComponent<BoxCollider>().isTrigger = true;

        }
        gameObject.GetComponent<BoxCollider>().size = size;
        


    }

    public bool InRangeOf90(int TestValue)
    {
        return (TestValue >= Mathf.Min(46, 90) && TestValue <= Mathf.Max(90, 134));
    }

    public bool InRangeOf180(int TestValue)
    {
        return (TestValue >= Mathf.Min(135, 180) && TestValue <= Mathf.Max(180, 224));
    }

    public bool InRangeOf270(int TestValue)
    {
        return (TestValue >= Mathf.Min(225, 270) && TestValue <= Mathf.Max(270, 314));
    }

    public bool InRangeOf360(int TestValue)
    {
        return (TestValue >= Mathf.Min(315, 360));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, size);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(distanceFromRoad, 0, 0), new Vector3(distanceFromRoad, 0, 50));
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(new Vector3(distanceFromRoad-14f, 0, 0), new Vector3(distanceFromRoad- 14f, 0, 50));
        Gizmos.DrawLine(new Vector3(distanceFromRoad+14f, 0, 0), new Vector3(distanceFromRoad+ 14f, 0, 50));
        Gizmos.color = Color.blue;
        var q = Quaternion.Euler(0, -YRotationOffset, 0);
        var fwd = q * Vector3.forward;
        Gizmos.DrawSphere(fwd * size.z, 4);



    }



}
