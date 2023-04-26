using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[ExecuteAlways]
public class SelfCheck : MonoBehaviour
{
    public UrbanMixerGenerator generator;

  


    public List<Collider> cols = new List<Collider>();
    public List<Collider> colliders(Transform trans, Vector3 size)
    {

        List<Collider> result = new List<Collider>();
        
        RaycastHit[] hits = Physics.BoxCastAll(trans.position, size/1.75f, Vector3.down);


        foreach (RaycastHit hit in hits)
        {
            Collider collider = hit.collider;
           
                if (result.Contains(collider))
                    continue;
                if (collider == trans.GetComponent<Collider>())
                    continue;

                // Skip Terrains
                Terrain terrain = collider.GetComponent<Terrain>();
                if (terrain != null)
                    continue;
                // If hit road
                UrbanMixerRoad Road = collider.GetComponent<UrbanMixerRoad>();

                if (Road != null)
                {
                    result.Add(trans.GetComponent<Collider>());
                    continue;
                }
            if (hit.collider.gameObject.GetComponent<BuildingType>() == null)
            {
                
                            continue;
                
                
                
            }






            if (collider.GetComponent<BuildingType>().ID < trans.GetComponent<BuildingType>().ID)
            {
                result.Add(trans.GetComponent<Collider>());
                continue;
            }

                   
               
        }

       
        return result;
    }

    public void Check()
    {
        if(generator == null)
        {
            generator = (UrbanMixerGenerator)GameObject.FindObjectOfType(typeof(UrbanMixerGenerator));
        }

        foreach(BuildingType building in GameObject.FindObjectsOfType(typeof(BuildingType)))
        {
            if (building != null)
            {
                
                
                    cols = colliders(building.transform, building.size);
                
            }
                if (cols.Count > 0)
                {
                    for (int i = 0; i < cols.Count; i++)
                    {
                        if (cols[i] != null)
                        {
                            DestroyImmediate(cols[i].transform.gameObject);
                        }


                    }
                }
        
        }

        }


       

    }



