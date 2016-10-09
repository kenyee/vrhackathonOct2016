using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class LazyCamController : MonoBehaviour {

	[SerializeField]
	Shader leftBlurShader;

	[SerializeField]
	Shader rightBlurShader;

	[SerializeField]
	private GvrViewer.Eye lazyEye = GvrViewer.Eye.Left;

	private BlurOptimized leftBlur;

	private BlurOptimized rightBlur;

	private Camera leftCam;

	private Camera rightCam;


	/// The singleton instance of the LazyCamController class.
	public static LazyCamController Instance {
		get {
			#if UNITY_EDITOR
			if (instance == null && !Application.isPlaying) {
				instance = UnityEngine.Object.FindObjectOfType<LazyCamController>();
			}
			#endif
			if (instance == null) {
				Debug.LogError("No LazyCamController instance found.  Ensure one exists in the scene, or call "
					+ "LazyCamController.Create() at startup to generate one.");
			}
			return instance;
		}
	}
	private static LazyCamController instance = null;

	/// Generate a LazyCamController instance.  Takes no action if one already exists.
	public static void Create() {
		if (instance == null && UnityEngine.Object.FindObjectOfType<LazyCamController>() == null) {
			Debug.Log("Creating LazyCamController object");
			var go = new GameObject("LazyCamController", typeof(GvrViewer));
			go.transform.localPosition = Vector3.zero;
			// sdk will be set by Awake().
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (leftCam == null) {
			var leftCamName = "Main Camera Left";
			var rightCamName = "Main Camera Right";

			// initialize cam info
			var leftCamObj = transform.FindChild(leftCamName).gameObject;
			leftCam = leftCamObj.GetComponent<Camera>();
			leftBlur = (BlurOptimized)leftCam.GetComponent(typeof(BlurOptimized));
			if (leftBlur == null) {
				leftBlur = leftCamObj.AddComponent(typeof(BlurOptimized)) as BlurOptimized;
			}
			leftBlur.blurShader = leftBlurShader;
			leftBlur.enabled = false;

			var rightCamObj = transform.FindChild (rightCamName).gameObject;
			rightCam = rightCamObj.GetComponent<Camera>();
			rightBlur = (BlurOptimized)rightCam.GetComponent(typeof(BlurOptimized));
			if (rightBlur == null) {
				rightBlur = rightCamObj.AddComponent(typeof(BlurOptimized)) as BlurOptimized;
			}
			rightBlur.blurShader = rightBlurShader;
			rightBlur.enabled = false;
		}

		ActivatEyeBlur ();
		leftCam.cullingMask = (leftCam.cullingMask & ~0x300) | 0x100;
		rightCam.cullingMask = (rightCam.cullingMask & ~0x300) | 0x200;
	}

	public void SwitchLazyEye() {
		switch (lazyEye) {
		case GvrViewer.Eye.Left:
			lazyEye = GvrViewer.Eye.Right;
			Debug.LogError ("Blurred Left Eye");
			break;
		case GvrViewer.Eye.Right:
			lazyEye = GvrViewer.Eye.Left;
			Debug.LogError ("Blurred Right Eye");
			break;
		}
	}

	private void ActivatEyeBlur() {
		if ((leftBlur == null) || (rightBlur == null)) {
			return;
		}

		switch (lazyEye) {
		case GvrViewer.Eye.Left:
			leftBlur.enabled = false;
			rightBlur.enabled = true;
			break;
		case GvrViewer.Eye.Right:
			leftBlur.enabled = true;
			rightBlur.enabled = false;
			break;
		}
	}
}
