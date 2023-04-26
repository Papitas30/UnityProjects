using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
	public enum Brain
	{
		Defender = 0,
		Flanker = 1,
		Hunter = 2
	}

	public enum State
	{
		Idle = 0,
		Wander = 1,
		Patrol = 2,
		Hunt = 3,
		Seek = 4,
		Defend = 5,
		Move = 6,
		Shift = 7,
		Flank = 8
	}

	public enum Combat
	{
		Locked = 0,
		Free = 1
	}

	[Header("PlayerData")]
	public PlayerData playerData;

	[Header("State")]
	public Brain brain;

	public State state;

	public Combat combat;

	[Header("Enemy Settings")]
	public float health;

	public float accuracy;

	public float reflex;

	public float damage;

	public float aimSpeed;

	public float turnSpeed;

	[Header("Enemy Activity")]
	public float distance;

	public bool isFlanking;

	public bool hasDefended;

	public bool hasShifted;

	public bool isHunting;

	public float defendTimer;

	private float deadSpeed;

	private float movementSpeed;

	[Header("Seek Settings")]
	public float seekRadius;

	public float seekTime;

	[Header("Shift Settings")]
	public float shiftRadius;

	[Header("Defend Settings")]
	public float defendTime;

	[Header("Weapon")]
	public Transform firepoint;

	public AudioSource weaponAudio;

	public AudioSource distantAudio;

	public ParticleSystem flash;

	public ParticleSystem smoke;

	public Light firelight;

	private float hitSpeed;

	private float hitIntensity;

	private bool hitPointChanged;

	[Header("Flank Positions")]
	public List<Transform> flankPosition;

	[HideInInspector]
	public NavMeshAgent agent;

	[HideInInspector]
	public Vector3 lastSeenPosition;

	[HideInInspector]
	public Vector3 lastHearPosition;

	private List<Transform> patrolPoints;

	private Vector3 previousPosition;

	private bool initialDestination;

	private float fireFrequency;

	private float seekTimer;

	private float fireTime;

	private bool isDead;

	private int nextWaypoint;

	private EnemyIK enemyIK;

	private Animator animator;

	private Sensor sensor;

	private int stateValue;

	private int currentState;

	private Vector3 direction;

	private bool hasMoved;

	private bool isSeeking;

	private int brainRoll;

	private Vector3 previousHearPosition;

	private Vector3 currentPosition;

	private bool aware;

	private bool initialFire;

	private bool hit;

	private bool recoverHit;

	public Transform hips;

	private Transform player;

	private AIRecoil recoil;

	private Ragdoll ragdoll;

	private AudioManager audioManager;

	private AIFootstep footstep;

	private MapManager mapManager;

	public bool sandbox;

	private void Start()
	{
		player = GameObject.FindWithTag("Player").transform;
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		mapManager = GameObject.FindWithTag("Map Manager").GetComponent<MapManager>();
		enemyIK = GetComponent<EnemyIK>();
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		sensor = GetComponent<Sensor>();
		ragdoll = GetComponent<Ragdoll>();
		footstep = GetComponent<AIFootstep>();
		fireTime = fireFrequency;
		state = State.Wander;
		hitSpeed = 2.5f;
		hitIntensity = 0.25f;
		brainRoll = Random.Range(0, 3);
		if (brainRoll == 1)
		{
			brain = Brain.Defender;
		}
		else if (brainRoll == 2)
		{
			brain = Brain.Flanker;
		}
		else
		{
			brain = Brain.Hunter;
		}
	}

	private void Update()
	{
		Hit();
		if (!sandbox)
		{
			distance = Vector3.Distance(base.transform.position, playerData.currentPosition);
			movementSpeed = (base.transform.position - previousPosition).magnitude / Time.deltaTime;
			previousPosition = base.transform.position;
			animator.SetFloat("Speed", movementSpeed);
			direction = base.transform.InverseTransformDirection(agent.velocity);
			animator.SetFloat("X", direction.x);
			animator.SetFloat("Y", direction.z);
			Turn();
			Aim();
			States();
			Weapon();
			Vision();
			Hearing();
			Randomize();
		}
	}

	private void Randomize()
	{
		if (stateValue != currentState)
		{
			currentState = stateValue;
			defendTime = Random.Range(1f, 10f);
			seekTime = Random.Range(25f, 60f);
		}
	}

	private void States()
	{
		if (state == State.Idle && !isDead)
		{
			agent.speed = 0f;
			agent.acceleration = 4f;
			agent.isStopped = true;
			agent.angularSpeed = 0f;
			agent.stoppingDistance = 2f;
			animator.SetBool("Combat", false);
			stateValue = 0;
			Randomize();
		}
		if (state == State.Wander && !isDead)
		{
			Wander();
			agent.speed = 2f;
			agent.acceleration = 4f;
			agent.stoppingDistance = 2f;
			agent.isStopped = false;
			agent.angularSpeed = 2000f;
			animator.SetBool("Combat", false);
			stateValue = 1;
			Randomize();
		}
		if (state == State.Patrol && !isDead)
		{
			Patrol();
			agent.speed = 2f;
			agent.acceleration = 4f;
			agent.angularSpeed = 2000f;
			agent.isStopped = false;
			agent.stoppingDistance = 2f;
			animator.SetBool("Combat", false);
			stateValue = 2;
			Randomize();
		}
		if (state == State.Defend && !isDead)
		{
			Defend();
			agent.speed = 0f;
			agent.acceleration = 4f;
			agent.angularSpeed = 0f;
			agent.stoppingDistance = 2f;
			agent.isStopped = false;
			animator.SetBool("Combat", true);
			stateValue = 5;
			Randomize();
		}
		if (state == State.Hunt && !isDead)
		{
			Hunt();
			agent.speed = 1f;
			agent.acceleration = 4f;
			agent.stoppingDistance = 2f;
			agent.isStopped = false;
			agent.angularSpeed = 0f;
			animator.SetBool("Combat", true);
			stateValue = 3;
			Randomize();
		}
		if (state == State.Seek && !isDead)
		{
			Seek();
			agent.speed = 1.8f;
			agent.acceleration = 4f;
			agent.angularSpeed = 2000f;
			agent.stoppingDistance = 2f;
			agent.isStopped = false;
			animator.SetBool("Combat", true);
			stateValue = 4;
			Randomize();
		}
		if (playerData.isDead)
		{
			state = State.Idle;
			agent.speed = 0f;
			agent.acceleration = 0f;
			agent.stoppingDistance = 0f;
			agent.isStopped = true;
			agent.destination = base.transform.position;
			agent.angularSpeed = 0f;
			combat = Combat.Locked;
			animator.SetBool("Combat", true);
			stateValue = 0;
		}
		if (state == State.Shift && !isDead)
		{
			Shift();
			agent.speed = 1f;
			agent.acceleration = 4f;
			agent.angularSpeed = 0f;
			agent.stoppingDistance = 2f;
			agent.isStopped = false;
			animator.SetBool("Combat", true);
			stateValue = 7;
			Randomize();
		}
		if (state == State.Flank && !isDead)
		{
			Flank();
			agent.speed = 6f;
			agent.acceleration = 6f;
			agent.angularSpeed = 2000f;
			agent.stoppingDistance = 2f;
			agent.isStopped = false;
			animator.SetBool("Combat", true);
			stateValue = 7;
			Randomize();
		}
	}

	private void Hearing()
	{
		if (lastHearPosition != previousHearPosition && lastHearPosition != lastSeenPosition && !aware)
		{
			previousHearPosition = lastHearPosition;
			lastSeenPosition = lastHearPosition;
			currentPosition = lastHearPosition;
			if (brain == Brain.Flanker && !isFlanking)
			{
				state = State.Flank;
			}
			if (brain == Brain.Hunter)
			{
				state = State.Defend;
			}
			if (brain == Brain.Defender)
			{
				state = State.Defend;
			}
			aware = true;
		}
	}

	private void Vision()
	{
		if (sensor.playerWithinLOS && !isFlanking)
		{
			combat = Combat.Free;
			currentPosition = playerData.currentPosition;
			lastHearPosition = playerData.currentPosition;
			lastSeenPosition = playerData.currentPosition;
			if (!hasDefended)
			{
				state = State.Defend;
				hasDefended = true;
			}
		}
		else
		{
			combat = Combat.Locked;
		}
	}

	private void Aim()
	{
		if (sensor.playerWithinLOS && animator.GetCurrentAnimatorStateInfo(0).IsName("Combat") && enemyIK.aimWeight < 1f)
		{
			enemyIK.aimTarget = currentPosition + new Vector3(0f, 1.4f, 0f);
			enemyIK.aimWeight += Time.deltaTime * aimSpeed;
		}
		else if (enemyIK.aimWeight > 0f)
		{
			enemyIK.aimWeight -= Time.deltaTime * 1f;
		}
	}

	private void Turn()
	{
		if (!isFlanking && (sensor.playerWithinLOS || state == State.Defend || state == State.Hunt))
		{
			Quaternion b = Quaternion.LookRotation(currentPosition - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, turnSpeed * Time.deltaTime);
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		}
	}

	private void Hunt()
	{
		agent.destination = lastSeenPosition;
		isHunting = true;
		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && !sensor.playerWithinLOS)
		{
			seekTimer = 0f;
			state = State.Seek;
			isHunting = false;
		}
	}

	private void Wander()
	{
		state = State.Wander;
		hasDefended = false;
		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
		{
			if (!initialDestination)
			{
				NavMeshHit navMeshHit;
				NavMesh.SamplePosition(_003CWander_003Eg__GetRandomLocation_007C78_0(), out navMeshHit, 20f, 1);
				agent.destination = navMeshHit.position;
				initialDestination = true;
			}
			else
			{
				state = State.Idle;
				Invoke("NextWander", 2f);
			}
		}
	}

	private void NextWander()
	{
		initialDestination = false;
		state = State.Wander;
	}

	private void Weapon()
	{
		if (combat == Combat.Free && enemyIK.aimWeight > reflex)
		{
			fireTime -= Time.deltaTime;
			if (fireTime <= 0f)
			{
				Fire();
			}
		}
	}

	private void Fire()
	{
		Vector3 forward = firepoint.forward;
		forward.x += Random.Range(0f - accuracy, accuracy);
		forward.y += Random.Range(0f - accuracy, accuracy);
		RaycastHit hitInfo;
		if (Physics.Raycast(firepoint.position, forward, out hitInfo, 100f))
		{
			if (hitInfo.collider.tag == "Player")
			{
				player.GetComponent<Damage>().TakeWeaponDamage();
			}
			else
			{
				Invoke("BulletFlyby", 0.05f);
			}
		}
		else
		{
			Invoke("BulletFlyby", 0.05f);
		}
		fireFrequency = Random.Range(0.1f, 1f);
		fireTime = fireFrequency;
		weaponAudio.Play();
		distantAudio.Play();
		flash.Play();
		smoke.Play();
		firelight.enabled = true;
		MuzzleFlash();
		Invoke("ResetFlash", 0.02f);
		weaponAudio.volume = 5f;
	}

	private void Patrol()
	{
		agent.destination = patrolPoints[nextWaypoint].position;
		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
		{
			nextWaypoint = (nextWaypoint + 1) % patrolPoints.Count;
		}
	}

	public void ApplyDamage(float damage, float impactForce, Vector3 hitDirection, Vector3 hitPoint, Transform IKTarget)
	{
		if (!sandbox)
		{
			health -= damage;
		}
		float num = 5f;
		float num2 = 0.5f;
		if (impactForce == 1f)
		{
			hitSpeed = num / 2f;
			hitIntensity = num2 / 2f;
		}
		else if (impactForce == 2f)
		{
			hitSpeed = num / 1.5f;
			hitIntensity = num2 / 1.5f;
		}
		else
		{
			hitSpeed = num;
			hitIntensity = num2;
		}
		enemyIK.ChangeHitPoint(IKTarget);
		hit = true;
		if (health <= 0f && !isDead)
		{
			GameObject.FindWithTag("Map Manager").GetComponent<MapManager>().AICount--;
			GameObject.FindWithTag("Map Manager").GetComponent<MapManager>().activeAI.Clear();
			ragdoll.RagdollModeOn(hitDirection, hitPoint);
			agent.isStopped = true;
			isDead = true;
			base.enabled = false;
		}
		if (isDead)
		{
			sensor.enabled = false;
			footstep.enabled = false;
			Invoke("AllowLooting", 1f);
		}
	}

	private void Seek()
	{
		seekTimer += Time.deltaTime;
		isSeeking = true;
		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
		{
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(Random.insideUnitSphere * seekRadius + playerData.currentPosition, out navMeshHit, seekRadius, 1);
			agent.destination = navMeshHit.position;
		}
		if (seekTimer > seekTime)
		{
			state = State.Wander;
			isSeeking = false;
		}
	}

	private void Defend()
	{
		defendTimer += Time.deltaTime;
		if (defendTimer > defendTime)
		{
			if (brain == Brain.Defender)
			{
				state = State.Shift;
			}
			if (brain == Brain.Flanker)
			{
				state = State.Flank;
			}
			if (brain == Brain.Hunter)
			{
				state = State.Hunt;
			}
			defendTimer = 0f;
		}
	}

	private void MuzzleFlash()
	{
		if (playerData.isNight)
		{
			firelight.intensity = 4f;
		}
		else
		{
			firelight.intensity = 1f;
		}
	}

	private void ResetFlash()
	{
		firelight.enabled = false;
	}

	private void AllowLooting()
	{
		hips.gameObject.layer = 7;
	}

	private void Flank()
	{
		if (distance < 25f)
		{
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(Random.insideUnitSphere * 2f + base.transform.position, out navMeshHit, 2f, 1);
			agent.destination = navMeshHit.position;
			animator.SetBool("Flank", false);
			enemyIK.enabled = true;
			isFlanking = false;
			brain = Brain.Defender;
			state = State.Defend;
			return;
		}
		if (!isFlanking)
		{
			animator.SetBool("Flank", true);
			enemyIK.enabled = false;
			mapManager.UpdateFlankPositions();
			flankPosition = mapManager.flankPositions;
			int index = Random.Range(0, flankPosition.Count);
			Transform transform = flankPosition[index];
			agent.destination = transform.position;
			isFlanking = true;
		}
		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
		{
			animator.SetBool("Flank", false);
			enemyIK.enabled = true;
			brain = Brain.Hunter;
			state = State.Hunt;
			isFlanking = false;
		}
	}

	private void Shift()
	{
		if (!hasShifted)
		{
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(Random.insideUnitSphere * shiftRadius + base.transform.position, out navMeshHit, shiftRadius, 1);
			agent.destination = navMeshHit.position;
			hasShifted = true;
		}
		if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
		{
			hasShifted = false;
			hasDefended = false;
			state = State.Defend;
		}
	}

	private void BulletFlyby()
	{
		audioManager.PlayBulletFlyby();
	}

	private void Hit()
	{
		if (hit)
		{
			enemyIK.aimWeight = 0f;
			if (enemyIK.hitWeight > hitIntensity)
			{
				recoverHit = true;
			}
			if (!recoverHit)
			{
				enemyIK.hitWeight += hitSpeed * Time.deltaTime;
			}
			else
			{
				enemyIK.hitWeight -= hitSpeed / 4f * Time.deltaTime;
			}
			if (recoverHit && enemyIK.hitWeight <= 0f)
			{
				hit = false;
				recoverHit = false;
			}
		}
	}

	[CompilerGenerated]
	internal static Vector3 _003CWander_003Eg__GetRandomLocation_007C78_0()
	{
		NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
		int num = Random.Range(0, navMeshTriangulation.indices.Length - 3);
		Vector3 vector = Vector3.Lerp(navMeshTriangulation.vertices[navMeshTriangulation.indices[num]], navMeshTriangulation.vertices[navMeshTriangulation.indices[num + 1]], Random.value);
		Vector3.Lerp(vector, navMeshTriangulation.vertices[navMeshTriangulation.indices[num + 2]], Random.value);
		return vector;
	}
}
