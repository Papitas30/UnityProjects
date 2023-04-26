using UnityEngine;

public class Fire : MonoBehaviour
{
	public PlayerData playerData;

	public GameObject campfireNoLit;

	public GameObject campfireLit;

	public bool isActive;

	public void Selected()
	{
		isActive = !isActive;
		if (isActive)
		{
			campfireNoLit.SetActive(false);
			campfireLit.SetActive(true);
		}
		else
		{
			campfireNoLit.SetActive(true);
			campfireLit.SetActive(false);
		}
	}
}
