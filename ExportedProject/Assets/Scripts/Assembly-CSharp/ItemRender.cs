using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemRender : MonoBehaviour
{
	[Tooltip("A folder will be created with this base name in your project root")]
	public string folderBaseName = "Screenshots";

	[Tooltip("How many frames should be captured per second of game time")]
	public int frameRate = 24;

	[Tooltip("How many frames should be captured before quitting")]
	public int framesToCapture = 24;

	private string folderName = "";

	private GameObject whiteCamGameObject;

	private Camera whiteCam;

	private UniversalAdditionalCameraData whiteCamAddData;

	private GameObject blackCamGameObject;

	private Camera blackCam;

	private UniversalAdditionalCameraData blackCamAddData;

	private Camera mainCam;

	private int videoFrame;

	private float originalTimescaleTime;

	private bool done;

	private int screenWidth;

	private int screenHeight;

	[SerializeField]
	private Texture2D textureBlack;

	[SerializeField]
	private Texture2D textureWhite;

	[SerializeField]
	private Texture2D textureTransparentBackground;

	private void Awake()
	{
		mainCam = base.gameObject.GetComponent<Camera>();
		CreateBlackAndWhiteCameras();
		CreateNewFolderForScreenshots();
		CacheAndInitialiseFields();
		Time.captureFramerate = frameRate;
	}

	private void LateUpdate()
	{
		if (!done)
		{
			StartCoroutine(CaptureFrame());
			return;
		}
		Debug.Log("Complete! " + videoFrame + " videoframes rendered. File names are 0 indexed)");
		Debug.Break();
	}

	private IEnumerator CaptureFrame()
	{
		yield return new WaitForEndOfFrame();
		if (videoFrame < framesToCapture)
		{
			RenderCamToTexture(whiteCam, textureWhite);
			RenderCamToTexture(blackCam, textureBlack);
			CalculateOutputTexture();
			SavePng();
			videoFrame++;
			Debug.Log("Rendered frame " + videoFrame);
		}
		else
		{
			done = true;
			StopCoroutine("CaptureFrame");
		}
	}

	private void RenderWiteRT2Texture()
	{
	}

	private void RenderCamToTexture(Camera cam, Texture2D tex)
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		RenderTexture renderTexture2 = (cam.targetTexture = new RenderTexture(screenWidth, screenHeight, 24));
		cam.Render();
		RenderTexture.active = renderTexture2;
		WriteScreenImageToTexture(tex);
		cam.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(renderTexture2);
		renderTexture2 = null;
	}

	private void WriteScreenImageToTexture(Texture2D tex)
	{
		tex.ReadPixels(new Rect(0f, 0f, screenWidth, screenHeight), 0, 0);
		tex.Apply();
	}

	private void CreateBlackAndWhiteCameras()
	{
		blackCamGameObject = new GameObject();
		blackCamGameObject.name = "Black Background Camera";
		blackCam = blackCamGameObject.AddComponent<Camera>();
		blackCamAddData = blackCamGameObject.AddComponent<UniversalAdditionalCameraData>();
		blackCamAddData.renderPostProcessing = true;
		blackCam.CopyFrom(mainCam);
		blackCam.backgroundColor = Color.black;
		blackCamGameObject.transform.SetParent(base.gameObject.transform, true);
		whiteCamGameObject = new GameObject();
		whiteCamGameObject.name = "White Background Camera";
		whiteCam = whiteCamGameObject.AddComponent<Camera>();
		whiteCamAddData = whiteCamGameObject.AddComponent<UniversalAdditionalCameraData>();
		whiteCamAddData.renderPostProcessing = true;
		whiteCam.CopyFrom(mainCam);
		whiteCam.backgroundColor = Color.white;
		whiteCamGameObject.transform.SetParent(base.gameObject.transform, true);
	}

	private void CreateNewFolderForScreenshots()
	{
		folderName = folderBaseName;
	}

	private void CalculateOutputTexture()
	{
		for (int i = 0; i < textureTransparentBackground.height; i++)
		{
			for (int j = 0; j < textureTransparentBackground.width; j++)
			{
				float num = textureWhite.GetPixel(j, i).r - textureBlack.GetPixel(j, i).r;
				num = 1f - num;
				Color color = ((num != 0f) ? (textureBlack.GetPixel(j, i) / 1f) : Color.clear);
				color.a = num;
				textureTransparentBackground.SetPixel(j, i, color);
			}
		}
	}

	private void SavePng()
	{
		string path = string.Format("{0}/{1:D04} shot.png", folderName, videoFrame);
		byte[] bytes = textureTransparentBackground.EncodeToPNG();
		File.WriteAllBytes(path, bytes);
	}

	private void CacheAndInitialiseFields()
	{
		originalTimescaleTime = Time.timeScale;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		textureBlack = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
		textureWhite = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
		textureTransparentBackground = new Texture2D(screenWidth, screenHeight, TextureFormat.ARGB32, false);
	}
}
