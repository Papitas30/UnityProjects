using UnityEngine;

public class DemoToggle : MonoBehaviour
{
	public GameObject targetA;

	public GameObject targetB;

	public GameObject targetC;

	public Light sun;

	public Transform targetSun;

	public bool lerp;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			targetA.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			targetB.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			targetC.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			lerp = true;
		}
		if (lerp)
		{
			sun.transform.LerpTrans(targetSun, Time.deltaTime * 1f);
		}
	}
}
