﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class ViveControllerInteraction : MonoBehaviour {

	private SteamVR_Controller.Device controller { get{ return SteamVR_Controller.Input ((int)trackedObj.index);}}
	private SteamVR_TrackedObject trackedObj;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private Mouse3DMovement mMouse;

	// Use this for initialization
	void Start () {
		mMouse = GameObject.Find ("Mouse3D").GetComponent<Mouse3DMovement> ();
		trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Raycast (move 3D mouse)
		RaycastHit hit;
		Ray ray = new Ray(transform.position,transform.forward);
		LayerMask onlyMousePlane = 1 << 8; // hit only the mouse plane layer

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, onlyMousePlane)) {
			//Vector3 offset = new Vector3(0.1f, 0.1f, 0.1f);
			mMouse.transform.position = hit.point;
			// Remember my UV coordinates, because the MouseUIInteraction script will use them to handle UI input:
			mMouse.setUVCoordinates(hit.textureCoord2);
		}

		//Detect trigger
		/*if (controller != null) {
			if (controller.GetPressDown (triggerButton)) {
				Debug.Log ("Trigger pressed");

			}

		} else {
			Debug.LogWarning ("Controller is null");
		}*/

	}
}
