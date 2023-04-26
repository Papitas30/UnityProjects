using UnityEngine;

public class SlideKick : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	public Vector3 kickPosition;

	public Vector3 initialPosition;

	public Vector3 emptyRotation;

	private Vector3 targetPosition;

	public bool barrel;

	public float speed;

	public bool empty;

	private Transform player;

	private Weapon weaponSystem;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		weaponSystem = player.GetComponent<Weapon>();
		weaponSystem.slideKicks.Add(this);
		initialPosition = Vector3.zero;
	}

	private void Update()
	{
		if (!barrel)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, targetPosition, Time.deltaTime * speed);
		}
		if (!barrel && base.transform.localPosition == targetPosition && !empty)
		{
			targetPosition = initialPosition;
		}
		if (empty)
		{
			base.transform.localEulerAngles = emptyRotation;
		}
	}

	public void ApplySlideKick()
	{
		targetPosition = kickPosition;
		if (barrel)
		{
			base.transform.position = kickPosition;
		}
		if (barrel && !empty)
		{
			Invoke("Return", 0.1f);
		}
	}

	public void Return()
	{
		if (!empty)
		{
			base.transform.localPosition = initialPosition;
		}
	}

	public void ApplyEmpty()
	{
		if (barrel)
		{
			base.transform.localPosition = kickPosition;
		}
		empty = true;
	}

	public void ReleaseSlide()
	{
		base.transform.localPosition = initialPosition;
		base.transform.localEulerAngles = Vector3.zero;
		empty = false;
	}

	private void OnDisable()
	{
		weaponSystem.slideKicks.Remove(this);
	}
}
