using System;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
	public float viewRadius;

	public float raycastHeight;

	public bool playerWithinLOS;

	public float distanceToTarget;

	[Range(0f, 360f)]
	public float viewAngle;

	public LayerMask targetMask;

	public LayerMask layerMaskFar;

	public LayerMask layerMaskNear;

	public LayerMask interactionLayer;

	public List<Transform> visibleTargets = new List<Transform>();

	public List<Transform> visibleDoors = new List<Transform>();

	public Collider[] targetsInRadius;

	public Collider[] interactableInRadius;

	public PlayerData playerData;

	private void Update()
	{
		FindVisibleDoors();
		FindVisibleTargets();
		if (playerData.isNight)
		{
			if (playerData.flashlightActive)
			{
				viewRadius = 100f;
			}
			else
			{
				viewRadius = 50f;
			}
		}
		else
		{
			viewRadius = 100f;
		}
	}

	private void FindVisibleDoors()
	{
		visibleDoors.Clear();
		interactableInRadius = Physics.OverlapSphere(base.transform.position, 4f, interactionLayer);
		for (int i = 0; i < interactableInRadius.Length; i++)
		{
			Transform transform = interactableInRadius[i].transform;
			if (transform.GetComponent<Interactable>().interactableType != Interactable.InteractableType.Door)
			{
				continue;
			}
			Door component = transform.GetComponent<Door>();
			if ((double)Vector3.Distance(component.transform.position, base.transform.position) < 2.5)
			{
				component.occupied = true;
				if (!component.isOpen)
				{
					component.AISelected();
					Debug.Log("AI Opened" + component.name);
				}
			}
			else
			{
				component.occupied = false;
			}
		}
	}

	private void FindVisibleTargets()
	{
		visibleTargets.Clear();
		targetsInRadius = Physics.OverlapSphere(base.transform.position, viewRadius, targetMask);
		playerWithinLOS = false;
		for (int i = 0; i < targetsInRadius.Length; i++)
		{
			Transform transform = targetsInRadius[i].transform;
			Vector3 normalized = (transform.position - base.transform.position).normalized;
			if (!(Vector3.Angle(base.transform.forward, normalized) < viewAngle / 2f))
			{
				continue;
			}
			distanceToTarget = Vector3.Distance(base.transform.position, transform.position);
			if (distanceToTarget > 40f)
			{
				if (!Physics.Raycast(base.transform.position + new Vector3(0f, raycastHeight, 0f), normalized, distanceToTarget, layerMaskFar))
				{
					visibleTargets.Add(transform);
					playerWithinLOS = true;
				}
				else
				{
					playerWithinLOS = false;
				}
			}
			if (distanceToTarget <= 40f)
			{
				if (!Physics.Raycast(base.transform.position + new Vector3(0f, raycastHeight, 0f), normalized, distanceToTarget, layerMaskNear))
				{
					visibleTargets.Add(transform);
					playerWithinLOS = true;
				}
				else
				{
					playerWithinLOS = false;
				}
			}
		}
	}

	public Vector3 directionFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += base.transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * (MathF.PI / 180f)), 0f, Mathf.Cos(angleInDegrees * (MathF.PI / 180f)));
	}
}
