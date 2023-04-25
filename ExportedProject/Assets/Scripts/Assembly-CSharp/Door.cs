using UnityEngine;

public class Door : MonoBehaviour
{
	[Header("Door Settings")]
	public bool inverse;

	public float openSpeed = 2f;

	[Header("Random Settings")]
	public bool isOpen;

	public bool useHandle;

	public bool isDirty;

	private AudioSource audioSource;

	private AudioManager audioManager;

	private Transform parent;

	private Transform handle;

	private float handleAngle;

	private float openAngle;

	private float defaultRotationAngle;

	private float currentRotationAngle;

	private float openTime;

	public bool occupied;

	private void Start()
	{
		parent = base.transform.parent;
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		if (useHandle)
		{
			handle = base.transform.GetChild(0).GetComponent<Transform>();
		}
		audioSource = base.transform.GetComponentInChildren<AudioSource>();
		defaultRotationAngle = parent.localEulerAngles.y;
		currentRotationAngle = parent.localEulerAngles.y;
	}

	public void Selected()
	{
		if (!occupied)
		{
			isOpen = !isOpen;
			audioManager.PlayDoorOpen(audioSource);
			currentRotationAngle = parent.localEulerAngles.y;
			openTime = 0f;
			if (inverse)
			{
				openAngle = 120f;
			}
			else
			{
				openAngle = -120f;
			}
		}
	}

	public void AISelected()
	{
		isOpen = !isOpen;
		audioManager.PlayDoorOpen(audioSource);
		currentRotationAngle = parent.localEulerAngles.y;
		openTime = 0f;
		if (inverse)
		{
			openAngle = 120f;
		}
		else
		{
			openAngle = -120f;
		}
	}

	private void Update()
	{
		if (openTime < 1f)
		{
			openTime += Time.deltaTime * openSpeed;
			if (isOpen)
			{
				handleAngle = Mathf.MoveTowardsAngle(handleAngle, 45f, 250f * Time.deltaTime);
			}
		}
		else
		{
			handleAngle = Mathf.MoveTowardsAngle(handleAngle, 0f, 250f * Time.deltaTime);
		}
		if (useHandle)
		{
			handle.localRotation = Quaternion.AngleAxis(handleAngle, Vector3.forward);
		}
		parent.localEulerAngles = new Vector3(parent.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (isOpen ? openAngle : 0f), openTime), parent.localEulerAngles.z);
	}
}
