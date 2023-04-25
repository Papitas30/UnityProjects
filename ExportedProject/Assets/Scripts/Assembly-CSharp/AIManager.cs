using UnityEngine;
using UnityEngine.UI;

public class AIManager : MonoBehaviour
{
	public enum AIDifficulty
	{
		Easy = 0,
		Normal = 1,
		Hard = 2
	}

	public AIDifficulty aiDifficulty;

	public Text AIText;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			aiDifficulty = AIDifficulty.Easy;
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			aiDifficulty = AIDifficulty.Normal;
		}
		if (Input.GetKeyDown(KeyCode.F3))
		{
			aiDifficulty = AIDifficulty.Hard;
		}
		if (aiDifficulty == AIDifficulty.Easy)
		{
			AIText.text = "1";
		}
		if (aiDifficulty == AIDifficulty.Normal)
		{
			AIText.text = "2";
		}
		if (aiDifficulty == AIDifficulty.Hard)
		{
			AIText.text = "3";
		}
	}
}
