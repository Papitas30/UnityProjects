using UnityEngine;

public class GunSway : MonoBehaviour
{

    public float amount;
    public float maxAmount;
    public float smoothAmount;
    public Vector3 swayVector;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }


    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new(movementX, movementY, 0);
        swayVector = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
        transform.localPosition = swayVector;
    }
}
