using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table", order = 3)]
public class LootTable : ScriptableObject
{
	public List<ItemData> itemList;
}
