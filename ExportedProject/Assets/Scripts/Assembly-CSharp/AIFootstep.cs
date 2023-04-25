using UnityEngine;

public class AIFootstep : MonoBehaviour
{
	[Header("Surface Layer")]
	public LayerMask surfaceLayer;

	[Header("Vertex Color")]
	public Color32 vertexColor;

	[Header("Audio Source")]
	public AudioSource audioSource;

	public AudioEvent footstepDirt;

	public AudioEvent footstepRock;

	public AudioEvent footstepWood;

	public AudioEvent footstepTile;

	private Mesh mesh;

	private int[] triangles;

	private int surfaceType;

	private Color32[] colors;

	private AI AI;

	private float loudness;

	private void Start()
	{
		AI = GetComponent<AI>();
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
				surfaceType = 1;
				vertexColor = new Color32(byte.MaxValue, 0, 0, 0);
			}
			else if (num2 > 50)
			{
				surfaceType = 2;
				vertexColor = new Color32(0, byte.MaxValue, 0, 0);
			}
			else if (num3 > 50)
			{
				surfaceType = 3;
				vertexColor = new Color32(0, 0, byte.MaxValue, 0);
			}
			else
			{
				surfaceType = 4;
				vertexColor = new Color32(0, 0, 0, 0);
			}
		}
		else
		{
			if (hitInfo.collider.tag == "Rock")
			{
				surfaceType = 5;
			}
			if (hitInfo.collider.tag == "Wood")
			{
				surfaceType = 6;
			}
			if (hitInfo.collider.tag == "Tile")
			{
				surfaceType = 7;
			}
		}
	}

	public void OnFootDown()
	{
		if (AI.agent.speed < 2f)
		{
			loudness = 1f;
		}
		else if (AI.agent.speed > 4f)
		{
			loudness = 2f;
		}
		else
		{
			loudness = 1.5f;
		}
		if (surfaceType == 4)
		{
			audioSource.Play();
		}
		if (surfaceType == 1)
		{
			footstepDirt.Play(audioSource, 1f);
		}
		if (surfaceType == 2)
		{
			footstepDirt.Play(audioSource, 1f);
		}
		if (surfaceType == 3)
		{
			footstepDirt.Play(audioSource, 1f);
		}
		if (surfaceType == 5)
		{
			footstepRock.Play(audioSource, 1f);
		}
		if (surfaceType == 6)
		{
			footstepWood.Play(audioSource, 1f);
		}
		if (surfaceType == 7)
		{
			footstepTile.Play(audioSource, 1f);
		}
	}
}
