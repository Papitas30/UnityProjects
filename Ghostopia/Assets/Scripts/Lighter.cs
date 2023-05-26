using UnityEngine;

public class Lighter : MonoBehaviour
{
    public GameObject lighter;
    public GameObject flames;
    

    private bool isOn;

    private void Start()
    {
        isOn = false;
        
        flames.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && lighter.activeInHierarchy && !isOn)
        {
            flames.SetActive(true);

            isOn = true;
            
        }

        else if (Input.GetButtonDown("Fire1") && lighter.activeInHierarchy && isOn)
        {
            flames.SetActive(false);

            isOn = false;
           
        }

    }
}