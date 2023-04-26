using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
	[Header("Tooltip Content")]
	public Image icon;

	public Text header;

	public Text subheader;

	public Text description;

	public Text stats;

	public Text highlight;

	[HideInInspector]
	public Transform target;

	[HideInInspector]
	public ItemData item;

	[HideInInspector]
	public Vector3 targetPoint;

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
		icon.sprite = item.icon;
		header.text = item.itemName;
		if (item.rarity == ItemData.ItemRarity.ExtremelyRare)
		{
			subheader.text = item.type.ToString() + " / Extremely Rare";
		}
		else
		{
			subheader.text = item.type.ToString() + " / " + item.rarity;
		}
		description.text = item.description;
		stats.text = "Volume: " + item.volume.ToString().Replace(",", ".") + "   /   Weight: " + item.weight.ToString().Replace(",", ".") + "   /   Value: " + item.value;
		highlight.text = item.name;
	}
}
