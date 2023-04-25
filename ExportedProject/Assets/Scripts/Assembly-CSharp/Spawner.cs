using UnityEngine;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
	[HideInInspector]
	public int spawnSize;

	[HideInInspector]
	public int spawnDensity;

	[HideInInspector]
	public Transform spawnParent;

	[HideInInspector]
	public float spawnHeight;

	[HideInInspector]
	public bool enableElementNaming;

	[HideInInspector]
	public bool enableVertexColors;

	[HideInInspector]
	public bool limitBlack;

	[HideInInspector]
	public bool limitRed;

	[HideInInspector]
	public bool limitGreen;

	[HideInInspector]
	public bool limitBlue;

	[HideInInspector]
	public int maxInterations = 1000;

	public Element[] elements;

	private Mesh mesh;

	private int[] triangles;

	private Color32[] colors;

	private MeshRenderer meshRenderer;

	private Material originalMaterial;

	private Material vertexColorMaterial;

	public void Spawn()
	{
		Clear();
		for (int i = 0; i < spawnSize; i += spawnDensity)
		{
			for (int j = 0; j < spawnSize; j += spawnDensity)
			{
				for (int k = 0; k < elements.Length; k++)
				{
					Element element = elements[k];
					if (!element.CanPlace())
					{
						continue;
					}
					GameObject gameObject = Object.Instantiate(element.GetRandom());
					Vector3 vector = new Vector3(Random.Range(-spawnSize / 2, spawnSize / 2), Random.Range(0, 0), Random.Range(-spawnSize / 2, spawnSize / 2));
					Vector3 vector2 = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
					Vector3 eulerAngles = new Vector3(Random.Range(0f, 4f), Random.Range(0f, 360f), Random.Range(0f, 4f));
					Vector3 localScale = Vector3.one * Random.Range(element.minScale, element.maxScale);
					if (enableElementNaming)
					{
						gameObject.name = element.name;
					}
					gameObject.transform.SetParent(spawnParent);
					gameObject.transform.position = vector + vector2;
					gameObject.transform.eulerAngles = eulerAngles;
					gameObject.transform.localScale = localScale;
					if (gameObject.transform.Find("Tree_Collider") != null)
					{
						gameObject.transform.Find("Tree_Collider").transform.localRotation = Quaternion.Euler(eulerAngles.x * -1f, 0f, eulerAngles.z * -1f);
					}
					RaycastHit hitInfo;
					if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, spawnHeight, 0f), Vector3.down, out hitInfo, 1000f))
					{
						if (hitInfo.collider.tag == "Terrain")
						{
							gameObject.transform.localPosition = hitInfo.point;
							if (element.surfaceRotation)
							{
								gameObject.transform.rotation = Quaternion.FromToRotation(base.transform.up, hitInfo.normal);
								float num = Random.Range(0f, 360f);
								gameObject.transform.Rotate(Vector3.up * num);
							}
							if (!enableVertexColors)
							{
								break;
							}
							mesh = hitInfo.collider.GetComponent<MeshFilter>().sharedMesh;
							triangles = mesh.triangles;
							colors = mesh.colors32;
							int triangleIndex = hitInfo.triangleIndex;
							Color32 color = colors[triangles[triangleIndex * 3]];
							Color32 color2 = colors[triangles[triangleIndex * 3 + 1]];
							Color32 color3 = colors[triangles[triangleIndex * 3 + 2]];
							int num2 = (color.r + color2.r + color3.r) / 3;
							int num3 = (color.g + color2.g + color3.g) / 3;
							int num4 = (color.b + color2.b + color3.b) / 3;
							if (num2 > 50)
							{
								if (limitRed)
								{
									Object.DestroyImmediate(gameObject);
								}
							}
							else if (num3 > 50)
							{
								if (limitGreen)
								{
									Object.DestroyImmediate(gameObject);
								}
							}
							else if (num4 > 50)
							{
								if (limitBlue)
								{
									Object.DestroyImmediate(gameObject);
								}
							}
							else if (limitBlack)
							{
								Object.DestroyImmediate(gameObject);
							}
						}
						else
						{
							Object.DestroyImmediate(gameObject);
						}
					}
					else
					{
						Object.DestroyImmediate(gameObject);
					}
					break;
				}
			}
		}
	}

	public void Clear()
	{
		for (int num = spawnParent.transform.childCount; num > 0; num--)
		{
			Object.DestroyImmediate(spawnParent.transform.GetChild(0).gameObject);
		}
	}

	public void ShowVertexColors()
	{
		originalMaterial = spawnParent.GetComponent<MeshRenderer>().sharedMaterial;
		vertexColorMaterial = Resources.Load("Debug", typeof(Material)) as Material;
		meshRenderer = spawnParent.GetComponent<MeshRenderer>();
		meshRenderer.material = vertexColorMaterial;
	}

	public void HideVertexColors()
	{
		meshRenderer = spawnParent.GetComponent<MeshRenderer>();
		meshRenderer.material = originalMaterial;
	}
}
