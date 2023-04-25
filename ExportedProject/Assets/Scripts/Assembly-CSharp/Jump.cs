using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{
	[Header("References")]
	public PlayerData playerData;

	public InputManager inputManager;

	public Transform head;

	public Transform weapon;

	public Vitals vitals;

	public Footstep footstep;

	[Header("Jump Settings")]
	[SerializeField]
	private AnimationCurve jumpFallOff;

	[SerializeField]
	private float jumpMultiplier;

	[Header("Jump Bob")]
	public float jumpBob = 2f;

	public float jumpRecovery = 10f;

	public Vector3 jumpBobBehavior = new Vector3(-10f, 4f, 4f);

	public Vector3 landBobBehavior = new Vector3(-10f, 4f, 4f);

	private Vector3 currentRotation;

	private Vector3 finalRotation;

	private Vector3 zeroRotation = new Vector3(-0.0001f, -0.0001f, -0.0001f);

	private AudioManager audioManager;

	private Controller controller;

	private CharacterController character;

	private void Start()
	{
		controller = GetComponent<Controller>();
		character = GetComponent<CharacterController>();
		footstep = GetComponent<Footstep>();
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
	}

	private void Update()
	{
		if (inputManager.jumpToggle && !playerData.isJumping && !controller.isSliding)
		{
			playerData.isJumping = true;
			if (footstep.surfaceType == 1)
			{
				audioManager.PlayFootstepGrassJump();
			}
			if (footstep.surfaceType == 2)
			{
				audioManager.PlayFootstepDirtJump();
			}
			if (footstep.surfaceType == 3)
			{
				audioManager.PlayFootstepAsphaltJump();
			}
			if (footstep.surfaceType == 4)
			{
				audioManager.PlayFootstepExtraJump();
			}
			if (footstep.surfaceType == 5)
			{
				audioManager.PlayFootstepConcreteJump();
			}
			if (footstep.surfaceType == 6)
			{
				audioManager.PlayFootstepConcreteJump();
			}
			if (footstep.surfaceType == 7)
			{
				audioManager.PlayFootstepRockJump();
			}
			if (footstep.surfaceType == 8)
			{
				audioManager.PlayFootstepWoodJump();
			}
			audioManager.PlayJumpGrunt();
			StartCoroutine(JumpEvent());
		}
		currentRotation = Vector3.Lerp(currentRotation, zeroRotation, jumpRecovery * Time.deltaTime);
		finalRotation = Vector3.Slerp(finalRotation, currentRotation, jumpBob * Time.deltaTime);
		head.localRotation = Quaternion.Euler(finalRotation);
		weapon.localRotation = Quaternion.Euler(finalRotation);
	}

	private IEnumerator JumpEvent()
	{
		character.slopeLimit = 90f;
		character.stepOffset = 0f;
		float timeInAir = 0f;
		vitals.ModifyBodyStamina(false, 5f);
		JumpKick();
		do
		{
			float num = jumpFallOff.Evaluate(timeInAir);
			character.Move(Vector3.up * num * jumpMultiplier * Time.deltaTime);
			timeInAir += Time.deltaTime;
			yield return null;
		}
		while (!character.isGrounded && character.collisionFlags != CollisionFlags.Above);
		character.slopeLimit = 60f;
		character.stepOffset = 0.4f;
		LandKick();
		playerData.isJumping = false;
		if (footstep.surfaceType == 1)
		{
			audioManager.PlayFootstepGrassLand();
		}
		if (footstep.surfaceType == 2)
		{
			audioManager.PlayFootstepDirtLand();
		}
		if (footstep.surfaceType == 3)
		{
			audioManager.PlayFootstepAsphaltLand();
		}
		if (footstep.surfaceType == 4)
		{
			audioManager.PlayFootstepExtraLand();
		}
		if (footstep.surfaceType == 5)
		{
			audioManager.PlayFootstepConcreteLand();
		}
		if (footstep.surfaceType == 6)
		{
			audioManager.PlayFootstepConcreteLand();
		}
		if (footstep.surfaceType == 7)
		{
			audioManager.PlayFootstepRockLand();
		}
		if (footstep.surfaceType == 8)
		{
			audioManager.PlayFootstepWoodLand();
		}
	}

	public void JumpKick()
	{
		currentRotation += new Vector3(jumpBobBehavior.x, jumpBobBehavior.y, jumpBobBehavior.z);
	}

	public void LandKick()
	{
		currentRotation += new Vector3(landBobBehavior.x, Random.Range(0f - landBobBehavior.y, landBobBehavior.y), Random.Range(0f - landBobBehavior.z, landBobBehavior.z));
	}
}
