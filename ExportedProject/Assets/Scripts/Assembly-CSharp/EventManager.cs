using UnityEngine;

public class EventManager : MonoBehaviour
{
	[Header("Game Events")]
	public GameEvent enterMap;

	public GameEvent exitMap;

	public GameEvent playerHasDied;

	public GameEvent playerIsDying;

	[Header("Lock Events")]
	public GameEvent lockPlayer;

	public GameEvent unlockPlayer;

	[Header("UI Events")]
	public GameEvent openUI;

	public GameEvent closeUI;

	public GameEvent openContainerUI;

	public GameEvent openPauseMenu;

	[Header("Medical Events")]
	public GameEvent fatique;

	public GameEvent bleeding;

	public GameEvent paint;

	private void Start()
	{
		enterMap.Raise();
	}
}
