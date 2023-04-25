using UnityEngine;

public class MGE_MoveThis : MonoBehaviour
{
	public float x_speed;

	public float x_rnd;

	public float y_speed;

	public float y_rnd;

	public float z_speed;

	public float z_rnd;

	private float x_actual;

	private float y_actual;

	private float z_actual;

	public bool localMovement = true;

	private void Start()
	{
		x_actual = Random.Range(x_speed - x_rnd, x_speed + x_rnd);
		y_actual = Random.Range(y_speed - y_rnd, y_speed + y_rnd);
		z_actual = Random.Range(z_speed - z_rnd, z_speed + z_rnd);
	}

	private void Update()
	{
		if (localMovement)
		{
			base.transform.Translate(x_actual * Time.deltaTime, y_actual * Time.deltaTime, z_actual * Time.deltaTime);
		}
		else
		{
			base.transform.Translate(x_actual * Time.deltaTime, y_actual * Time.deltaTime, z_actual * Time.deltaTime, Space.World);
		}
	}
}
