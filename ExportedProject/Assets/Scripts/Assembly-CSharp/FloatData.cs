using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Float", order = 3)]
public class FloatData : ScriptableObject
{
	public float Value;

	public void SetValue(float value)
	{
		Value = value;
	}

	public void SetValue(FloatData value)
	{
		Value = value.Value;
	}

	public void ApplyChange(float amount)
	{
		Value += amount;
	}

	public void ApplyChange(FloatData amount)
	{
		Value += amount.Value;
	}
}
