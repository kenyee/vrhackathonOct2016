using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class EyeBlurToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Toggle() {
		var leftCamObj = GameObject.Find("LeftCam").gameObject;
		var leftCam = leftCamObj.GetComponent<Camera>();
		var leftBlur = (BlurOptimized)leftCam.GetComponent(typeof(BlurOptimized));
		leftBlur.enabled = !leftBlur.enabled;

		var rightCamObj = GameObject.Find ("RightCam").gameObject;
		var rightCam = rightCamObj.GetComponent<Camera>();
		var rightBlur = (BlurOptimized)rightCam.GetComponent(typeof(BlurOptimized));
		rightBlur.enabled = !rightBlur.enabled;
	}
}
