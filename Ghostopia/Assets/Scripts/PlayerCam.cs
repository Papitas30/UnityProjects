using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    float mouseX;
    float mouseY;
    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Update()
    {

        // get mouse input
         mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
         mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;

        xRotation -= mouseY;

    }
    public void FixedUpdate()
    {
       
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}