using UnityEngine;

public class AIRecoil : MonoBehaviour
{
	private EnemyIK enemyIK;

	public float recoilAmount;

	public float recoveryThreshold;

	public float recoilSpeed;

	private float targetOffset;

	public bool recoil;

	private void Start()
	{
		enemyIK = GetComponent<EnemyIK>();
	}

	private void Update()
	{
		if (recoil)
		{
			targetOffset = recoilAmount;
			if (enemyIK.aimTargetOffset.y >= recoveryThreshold)
			{
				recoil = false;
			}
		}
		else
		{
			targetOffset = 0f;
		}
		enemyIK.aimTargetOffset.y = Mathf.Lerp(enemyIK.aimTargetOffset.y, targetOffset, recoilSpeed * Time.deltaTime);
	}

	public void ApplyRecoil()
	{
		recoil = true;
	}
}
