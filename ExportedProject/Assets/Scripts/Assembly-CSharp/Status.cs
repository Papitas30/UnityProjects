using UnityEngine;

public class Status : MonoBehaviour
{
	[Header("Gizmos")]
	public Transform destination;

	public Transform lastSeen;

	public Transform lastHear;

	[Header("Status")]
	public GameObject idle;

	public GameObject wander;

	public GameObject hunt;

	public GameObject defend;

	public GameObject flank;

	public GameObject move;

	public GameObject shift;

	public GameObject seek;

	private AI AI;

	private void Start()
	{
		AI = GetComponent<AI>();
		destination.gameObject.SetActive(true);
		lastSeen.gameObject.SetActive(true);
		lastHear.gameObject.SetActive(true);
	}

	private void Update()
	{
		destination.position = AI.agent.destination + new Vector3(0f, 2f, 0f);
		lastSeen.position = AI.lastSeenPosition + new Vector3(0f, 1.25f, 0f);
		lastHear.position = AI.lastHearPosition + new Vector3(0f, 0.5f, 0f);
		if (AI.state == AI.State.Idle)
		{
			idle.SetActive(true);
			wander.SetActive(false);
			defend.SetActive(false);
			move.SetActive(false);
			shift.SetActive(false);
			hunt.SetActive(false);
			seek.SetActive(false);
		}
		if (AI.state == AI.State.Wander)
		{
			idle.SetActive(false);
			wander.SetActive(true);
			defend.SetActive(false);
			move.SetActive(false);
			shift.SetActive(false);
			hunt.SetActive(false);
			seek.SetActive(false);
		}
		if (AI.state == AI.State.Defend)
		{
			idle.SetActive(false);
			wander.SetActive(false);
			defend.SetActive(true);
			move.SetActive(false);
			shift.SetActive(false);
			hunt.SetActive(false);
			seek.SetActive(false);
		}
		if (AI.state == AI.State.Move)
		{
			idle.SetActive(false);
			wander.SetActive(false);
			defend.SetActive(false);
			move.SetActive(true);
			shift.SetActive(false);
			hunt.SetActive(false);
			seek.SetActive(false);
		}
		if (AI.state == AI.State.Shift)
		{
			idle.SetActive(false);
			wander.SetActive(false);
			defend.SetActive(false);
			move.SetActive(false);
			shift.SetActive(true);
			hunt.SetActive(false);
			seek.SetActive(false);
		}
		if (AI.state == AI.State.Hunt)
		{
			idle.SetActive(false);
			wander.SetActive(false);
			defend.SetActive(false);
			move.SetActive(false);
			shift.SetActive(false);
			hunt.SetActive(true);
			seek.SetActive(false);
		}
		if (AI.state == AI.State.Seek)
		{
			idle.SetActive(false);
			wander.SetActive(false);
			defend.SetActive(false);
			move.SetActive(false);
			shift.SetActive(false);
			hunt.SetActive(false);
			seek.SetActive(true);
		}
	}
}
