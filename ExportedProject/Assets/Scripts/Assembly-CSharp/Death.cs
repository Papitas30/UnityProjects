using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
	public PlayerData playerData;

	public Transform headTransform;

	public Rigidbody head;

	public Collider headCollider;

	private AudioManager audioManager;

	private Fader fader;

	public GameObject core;

	public Transform originalParent;

	public Equipment equipment;

	private void Start()
	{
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		equipment = GetComponent<Equipment>();
		originalParent = head.transform.parent;
		headCollider.isTrigger = true;
		head.isKinematic = true;
		head.useGravity = false;
	}

	public void PlayerDeath()
	{
		headCollider.isTrigger = false;
		playerData.isFreezed = true;
		playerData.isDead = true;
		head.transform.parent = null;
		head.useGravity = true;
		head.isKinematic = false;
		head.AddForce(base.transform.forward * 2f, ForceMode.Impulse);
		head.AddForce(-base.transform.up * 2f, ForceMode.Impulse);
		head.AddTorque(-base.transform.forward * -1f * 1f, ForceMode.Impulse);
		playerData.isDead = true;
		Renderer[] componentsInChildren = headTransform.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		audioManager.PlayDeathFall();
		fader = GameObject.FindWithTag("Fader").GetComponent<Fader>();
		fader.FadeIn();
		Invoke("Execute", 2.5f);
	}

	private void Execute()
	{
		if (playerData.primaryActive)
		{
			equipment.DeactivatePrimary();
		}
		if (playerData.secondaryActive)
		{
			equipment.DeactivateSecondary();
		}
		headCollider.isTrigger = true;
		playerData.isFreezed = false;
		playerData.isDead = false;
		head.transform.parent = originalParent;
		head.transform.localPosition = Vector3.zero;
		head.useGravity = false;
		head.isKinematic = true;
		playerData.isDead = false;
		SceneManager.LoadScene("Sandbox");
	}
}
