using UnityEngine;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour
{
	private Material material;

	public EventTrigger eventTrigger;

	private void Start()
	{
		eventTrigger = GetComponent<EventTrigger>();
		eventTrigger.enabled = false;
		material = GetComponent<Renderer>().material;
	}

	public void HighlightOFF()
	{
		material.SetFloat("_Intensity", 0f);
	}

	public void HighlightON()
	{
		material.SetFloat("_Intensity", 0.5f);
	}

	public void Activate()
	{
		eventTrigger.enabled = true;
	}
}
