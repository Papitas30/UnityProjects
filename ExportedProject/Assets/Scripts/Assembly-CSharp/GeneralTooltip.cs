using UnityEngine;
using UnityEngine.UI;

public class GeneralTooltip : MonoBehaviour
{
	[Header("Tooltip Content")]
	public string text;

	public Text content;

	[HideInInspector]
	public Transform target;

	[HideInInspector]
	public Vector3 targetPoint;

	[HideInInspector]
	public ItemData item;

	private void LateUpdate()
	{
		if (target != null)
		{
			Vector3 position = Camera.main.WorldToScreenPoint(targetPoint);
			base.transform.position = position;
		}
	}

	public void UpdateTooltipUI()
	{
		content.text = text;
	}
}
