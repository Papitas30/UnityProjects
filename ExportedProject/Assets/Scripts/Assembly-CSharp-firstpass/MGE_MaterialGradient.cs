using UnityEngine;

public class MGE_MaterialGradient : MonoBehaviour
{
	public Gradient myGradient;

	public int targetMaterialSlot;

	public float speed = 1f;

	public bool tintColor = true;

	public bool mainColor;

	public bool loop;

	public bool resetTimeOnActivation;

	private float time;

	private Color curColor;

	private void Start()
	{
		curColor = myGradient.Evaluate(time);
		if (tintColor)
		{
			GetComponent<Renderer>().materials[targetMaterialSlot].SetColor("_TintColor", curColor);
		}
		if (mainColor)
		{
			GetComponent<Renderer>().materials[targetMaterialSlot].SetColor("_Color", curColor);
		}
	}

	private void Update()
	{
		time += Time.deltaTime * speed;
		curColor = myGradient.Evaluate(time);
		if (tintColor)
		{
			GetComponent<Renderer>().materials[targetMaterialSlot].SetColor("_TintColor", curColor);
		}
		if (mainColor)
		{
			GetComponent<Renderer>().materials[targetMaterialSlot].SetColor("_Color", curColor);
		}
		if (loop && time >= 1f)
		{
			time -= 1f;
		}
	}

	private void OnEnable()
	{
		if (resetTimeOnActivation)
		{
			time = 0f;
		}
	}
}
