using UnityEngine;

public class Interactor : MonoBehaviour
{
	[Header("Settings")]
	[Range(0f, 10f)]
	public float interactionDistance;

	public LayerMask interactionLayer;

	public LayerMask surfaceLayer;

	public PlayerData playerData;

	public Placer placer;

	[Header("Tooltip UI")]
	public Canvas tooltipUI;

	public ItemTooltip itemTooltip;

	public GeneralTooltip generalTooltip;

	[Header("Target")]
	public Interactable interactionTarget;

	public Interactable selected;

	public Interactable previousTarget;

	public Interactable previousSelected;

	private Camera mainCamera;

	private InputManager inputManager;

	private void Start()
	{
		mainCamera = Camera.main.GetComponent<Camera>();
		inputManager = GameObject.FindWithTag("Input Manager").GetComponent<InputManager>();
	}

	private void Update()
	{
		mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hitInfo;
		if (Physics.SphereCast(mainCamera.transform.position, 0.1f, mainCamera.transform.forward, out hitInfo, interactionDistance, interactionLayer) && !inputManager.UIToggle && !inputManager.pauseMenuToggle && !playerData.isPlacing)
		{
			RaycastHit hitInfo2;
			if (Physics.Raycast(mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hitInfo2, hitInfo.distance, surfaceLayer))
			{
				if (interactionTarget != null)
				{
					interactionTarget = null;
				}
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(false);
				itemTooltip.target = null;
				generalTooltip.target = null;
				tooltipUI.enabled = false;
				return;
			}
			if (interactionTarget != null)
			{
				previousTarget = interactionTarget;
			}
			interactionTarget = hitInfo.collider.GetComponent<Interactable>();
			if (interactionTarget.interactableType == Interactable.InteractableType.Pickup && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				placer.lookObject = interactionTarget.gameObject;
				itemTooltip.gameObject.SetActive(true);
				generalTooltip.gameObject.SetActive(false);
				itemTooltip.item = interactionTarget.GetComponent<Pickup>().item;
				itemTooltip.UpdateTooltipUI();
				itemTooltip.target = interactionTarget.transform;
				itemTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Container && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				if (interactionTarget.GetComponent<Container>().items.Count == 0)
				{
					generalTooltip.text = interactionTarget.GetComponent<Container>().containerName + " (Empty)";
				}
				else
				{
					generalTooltip.text = interactionTarget.GetComponent<Container>().containerName;
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Door && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<BoxCollider>().bounds.center;
				if (interactionTarget.GetComponent<Door>().occupied)
				{
					generalTooltip.text = "AI Occupied";
				}
				else if (!interactionTarget.GetComponent<Door>().isOpen)
				{
					generalTooltip.text = "Open";
				}
				else
				{
					generalTooltip.text = "Close";
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Radio && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				placer.lookObject = interactionTarget.gameObject;
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				if (!interactionTarget.GetComponent<Radio>().isActive)
				{
					generalTooltip.text = "Turn On";
				}
				else
				{
					generalTooltip.text = "Turn Off";
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Television && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				if (!interactionTarget.GetComponent<Television>().isActive)
				{
					generalTooltip.text = "Turn On";
				}
				else
				{
					generalTooltip.text = "Turn Off";
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Fire && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				if (!interactionTarget.GetComponent<Fire>().isActive)
				{
					generalTooltip.text = "Ignite";
				}
				else
				{
					generalTooltip.text = "Extinguish";
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Candle && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				placer.lookObject = interactionTarget.gameObject;
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				if (!interactionTarget.GetComponent<Fire>().isActive)
				{
					generalTooltip.text = "Ignite";
				}
				else
				{
					generalTooltip.text = "Extinguish";
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
			if (interactionTarget.interactableType == Interactable.InteractableType.Boat && !inputManager.UIToggle && !inputManager.pauseMenuToggle)
			{
				itemTooltip.gameObject.SetActive(false);
				generalTooltip.gameObject.SetActive(true);
				generalTooltip.target = interactionTarget.transform;
				generalTooltip.targetPoint = interactionTarget.GetComponent<Collider>().bounds.center;
				if (!interactionTarget.GetComponent<Boat>().isActive)
				{
					generalTooltip.text = "Start Motor";
				}
				else
				{
					generalTooltip.text = "Turn Off";
				}
				generalTooltip.UpdateTooltipUI();
				tooltipUI.enabled = true;
			}
		}
		else
		{
			if (interactionTarget != null)
			{
				interactionTarget = null;
			}
			placer.lookObject = null;
			itemTooltip.gameObject.SetActive(false);
			generalTooltip.gameObject.SetActive(false);
			itemTooltip.target = null;
			generalTooltip.target = null;
			tooltipUI.enabled = false;
		}
	}

	public void Interact()
	{
		if (selected != previousSelected)
		{
			previousSelected.tag = "Untagged";
		}
		if (interactionTarget != null && selected == null)
		{
			selected = interactionTarget;
			selected.Interact();
			previousSelected = selected;
			selected = null;
		}
		if (interactionTarget == null)
		{
			interactionTarget = null;
			previousTarget = null;
			selected = null;
			previousSelected = null;
		}
	}
}
