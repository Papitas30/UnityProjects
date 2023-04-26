using UnityEngine;

public class Hotkeys : MonoBehaviour
{
	public GameEvent alpha1;

	public GameEvent alpha2;

	public GameEvent alpha3;

	public GameEvent alpha4;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			alpha1.Raise();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			alpha2.Raise();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			alpha3.Raise();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			alpha4.Raise();
		}
		Input.GetKeyDown(KeyCode.Tab);
		Input.GetKeyDown(KeyCode.I);
		Input.GetKeyDown(KeyCode.B);
		Input.GetKeyDown(KeyCode.M);
		Input.GetKeyDown(KeyCode.F);
		Input.GetKeyDown(KeyCode.N);
		Input.GetKeyDown(KeyCode.C);
	}
}
