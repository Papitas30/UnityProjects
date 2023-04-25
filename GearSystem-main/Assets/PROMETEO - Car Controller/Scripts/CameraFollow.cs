using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform carTransform;
    [Range(0.5f, 50)]
    public float followSpeed = 2;
    [Range(0.5f, 50)]
    public float lookSpeed = 5;
    private Vector3 initialCameraPosition;
    private Vector3 initialCarPosition;
    private Vector3 absoluteInitCameraPosition;
    private Vector3 targetPos;
    private Quaternion rot;

    private void Start()
    {
        initialCameraPosition = gameObject.transform.position;
        initialCarPosition = carTransform.position;
        absoluteInitCameraPosition = initialCameraPosition - initialCarPosition;
    }

    private void Update()
    {
        //Look at car
        Vector3 lookDirection = new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z) - transform.position;
        rot = Quaternion.LookRotation(lookDirection, Vector3.up);


        //Move to car
        targetPos = absoluteInitCameraPosition + carTransform.transform.position;


    }
    private void LateUpdate()
    {
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime), Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime));
    }
}
