using UnityEngine;

public class Footstep : MonoBehaviour
{
	[Header("Surface Layer")]
	public LayerMask surfaceLayer;

	[Header("Vertex Color")]
	public Color32 vertexColor;

	[Header("Audio Manager")]
	public AudioManager audioManager;

	private Mesh mesh;

	private int[] triangles;

	[HideInInspector]
	public int surfaceType;

	private Color32[] colors;

	public PlayerData playerData;

	private Controller controller;

	private Sound soundwave;

	private void Start()
	{
		controller = GetComponent<Controller>();
		soundwave = GetComponent<Sound>();
	}

	private void Update()
	{
		RaycastHit hitInfo;
		if (!Physics.Raycast(base.transform.position + new Vector3(0f, 1f, 0f), Vector3.down, out hitInfo, 2f, surfaceLayer))
		{
			return;
		}
		if (hitInfo.collider.tag == "Terrain")
		{
			mesh = hitInfo.collider.GetComponent<MeshFilter>().mesh;
			triangles = mesh.triangles;
			colors = mesh.colors32;
			int triangleIndex = hitInfo.triangleIndex;
			Color32 color = colors[triangles[triangleIndex * 3]];
			Color32 color2 = colors[triangles[triangleIndex * 3 + 1]];
			Color32 color3 = colors[triangles[triangleIndex * 3 + 2]];
			int num = (color.r + color2.r + color3.r) / 3;
			int num2 = (color.g + color2.g + color3.g) / 3;
			int num3 = (color.b + color2.b + color3.b) / 3;
			if (num > 50)
			{
				surfaceType = 2;
				vertexColor = new Color32(byte.MaxValue, 0, 0, 0);
			}
			else if (num2 > 50)
			{
				surfaceType = 3;
				vertexColor = new Color32(0, byte.MaxValue, 0, 0);
			}
			else if (num3 > 50)
			{
				surfaceType = 4;
				vertexColor = new Color32(0, 0, byte.MaxValue, 0);
			}
			else
			{
				surfaceType = 1;
				vertexColor = new Color32(0, 0, 0, 0);
			}
			playerData.hardsurface = false;
			playerData.woodSurface = false;
			controller.rockSurface = false;
		}
		else
		{
			if (hitInfo.collider.tag == "Concrete")
			{
				surfaceType = 5;
				controller.rockSurface = false;
				playerData.hardsurface = true;
				playerData.woodSurface = false;
			}
			if (hitInfo.collider.tag == "Metal")
			{
				surfaceType = 6;
				controller.rockSurface = false;
				playerData.hardsurface = true;
				playerData.woodSurface = false;
			}
			if (hitInfo.collider.tag == "Rock")
			{
				surfaceType = 7;
				controller.rockSurface = true;
				playerData.hardsurface = true;
				playerData.woodSurface = false;
			}
			if (hitInfo.collider.tag == "Wood")
			{
				surfaceType = 8;
				controller.rockSurface = false;
				playerData.hardsurface = true;
				playerData.woodSurface = true;
			}
		}
	}

	public void OnFootDown()
	{
		if (controller.isGrounded)
		{
			soundwave.SetSoundwave();
			if (surfaceType == 1)
			{
				audioManager.PlayFootstepGrass();
			}
			if (surfaceType == 2)
			{
				audioManager.PlayFootstepDirt();
			}
			if (surfaceType == 3)
			{
				audioManager.PlayFootstepAsphalt();
			}
			if (surfaceType == 4)
			{
				audioManager.PlayFootstepExtra();
			}
			if (surfaceType == 5)
			{
				audioManager.PlayFootstepConcrete();
			}
			if (surfaceType == 6)
			{
				audioManager.PlayFootstepMetal();
			}
			if (surfaceType == 7)
			{
				audioManager.PlayFootstepRock();
			}
			if (surfaceType == 8)
			{
				audioManager.PlayFootstepWood();
			}
		}
	}
}
