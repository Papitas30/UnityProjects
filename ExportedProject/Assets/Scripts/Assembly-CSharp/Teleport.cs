using UnityEngine;

public class Teleport : MonoBehaviour
{
	[Header("Target")]
	public Transform target;

	[Header("Teleport Clock")]
	public float teleportTime;

	[Header("Teleport Values")]
	public float teleportFrequency;

	[Range(0f, 10f)]
	public float maxXDistance;

	[Range(0f, 10f)]
	public float maxYDistance;

	private Vector2 randomPosition;

	private void Start()
	{
		teleportTime = teleportFrequency;
	}

	private void Update()
	{
		teleportTime -= Time.deltaTime;
		if (teleportTime <= 0f)
		{
			ExecuteTeleport();
		}
	}

	private void ExecuteTeleport()
	{
		teleportTime = teleportFrequency;
		float x = Random.Range(0f - maxXDistance, maxXDistance);
		float y = Random.Range(0f - maxYDistance, maxYDistance);
		randomPosition = new Vector2(x, y);
		target.position = randomPosition;
	}
}
