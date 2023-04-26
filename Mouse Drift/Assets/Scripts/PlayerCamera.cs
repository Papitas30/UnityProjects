using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform car;
    public float distance = 6.4f;
    public float height = 1.4f;
    public float rotationDamping = 3.0f;
    public float heightDamping = 2.0f;
   

    private Vector3 rotationVector;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void LateUpdate()
    {
        float wantedAngle = rotationVector.y;
        float wantedHeight = car.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        Vector3 temp = transform.position; //temporary variable so Unity doesn't complain
        temp.y = myHeight;
        transform.position = temp;
        transform.LookAt(car);
    }

    void FixedUpdate()
    {
       Vector3 previousPosition = car.position;
         Vector3 worldVelocity = (car.position - previousPosition) / Time.deltaTime;
       
      
        if (worldVelocity.z < -0.1f)
        {
            Vector3 temp = rotationVector; //because temporary variables seem to be removed after a closing bracket "}" we can use the same variable name multiple times.
            temp.y = car.eulerAngles.y + 180;
            rotationVector = temp;
        }
        else
        {
            Vector3 temp = rotationVector;
            temp.y = car.eulerAngles.y;
            rotationVector = temp;
        }
      
    }
}