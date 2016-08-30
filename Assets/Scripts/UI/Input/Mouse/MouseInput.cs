﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour, InputDeviceInterface {

    private PointerEventData.FramePressState leftButtonState = PointerEventData.FramePressState.NotChanged;
    private PointerEventData.FramePressState middleButtonState = PointerEventData.FramePressState.NotChanged;
    private PointerEventData.FramePressState rightButtonState = PointerEventData.FramePressState.NotChanged;
    private bool visualizeMouseRay = true;

    private LineRenderer lineRenderer;

	public bool developmentMode = true;
	private float mouseSpeed = 0.04f;
	private Vector3 lastPos = new Vector3(0,0,0);

    public void activateVisualization()
    {
        visualizeMouseRay = true;
    }

    public void deactivateVisualization()
    {
        visualizeMouseRay = false;
		Vector3 zero = new Vector3(0, 0, 0);
		lineRenderer.SetPosition(0, zero);
		lineRenderer.SetPosition(1, zero);
    }

    public Ray createRay()
    {
        Ray ray;
		if(developmentMode){
        	ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
		else{ //TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			Vector3 dir = lastPos - Camera.main.transform.position + new Vector3(Input.GetAxis("Mouse X") * mouseSpeed, Input.GetAxis("Mouse Y") * mouseSpeed, 0);
			ray = new Ray(Camera.main.transform.position, dir); 
		}
        return ray;
    }

    public PointerEventData.FramePressState getLeftButtonState()
    {
        return mouseButtonStateHandler(leftButtonState, 0);
    }


    public PointerEventData.FramePressState getMiddleButtonState()
    {
        return mouseButtonStateHandler(middleButtonState, 2);
    }

    public PointerEventData.FramePressState getRightButtonState()
    {
        return mouseButtonStateHandler(rightButtonState, 1);
    }

    public Vector2 getScrollDelta()
    {
        return Input.mouseScrollDelta;
    }

    public RaycastHit getRaycastHit()
    {
        RaycastHit hit;
        Ray ray = createRay();       
        //LayerMask onlyMousePlane = 1 << 8; // hit only the mouse plane layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
			if (hit.collider.gameObject.layer == 8) {//TODO
				lastPos = hit.point;//TODO
			}
			return hit;
        }
        else
        {
            Debug.LogError("No hit found. Can not return currect UV Coordiantes");
			if (hit.collider.gameObject.layer == 8) {//TODO
				lastPos = hit.point; //TODO
			}
            return hit;
        }
    }

    public bool isVisualizerActive()
    {
        return visualizeMouseRay;
    }

    // Use this for initialization
    void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("[MouseInput.cs] Line renderer not set");
        }

        InputDeviceManager idm = GameObject.Find("GlobalScript").GetComponent<InputDeviceManager>();
        if (idm != null)
        {
            idm.registerInputDevice(this.gameObject);
            Debug.Log("Mouse registered");
        }


		//TODO -----------------------------------
		RaycastHit hit;
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);       
		//LayerMask onlyMousePlane = 1 << 8; // hit only the mouse plane layer
		Physics.Raycast(ray, out hit, Mathf.Infinity);
		lastPos = hit.point;


		//----------------------------------------

    }
	
	// Update is called once per frame
	void Update () {
        if (visualizeMouseRay)
        {
            RaycastHit hit;
            Ray ray = createRay();
            //LayerMask onlyMousePlane = 1 << 8; // hit only the mouse plane layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 offset = new Vector3(0.4f, -0.4f, 0);
                lineRenderer.SetPosition(0, Camera.main.transform.position + offset);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
    }

    private PointerEventData.FramePressState mouseButtonStateHandler(PointerEventData.FramePressState s, int buttonID)
    {
        PointerEventData.FramePressState result = PointerEventData.FramePressState.NotChanged;
        switch (s)
        {
            case PointerEventData.FramePressState.NotChanged:
                if (Input.GetMouseButtonDown(buttonID))
                {
                    result = PointerEventData.FramePressState.Pressed;
                }
                if (Input.GetMouseButtonUp(buttonID))
                {
                    result = PointerEventData.FramePressState.Released;
                }
                break;
            case PointerEventData.FramePressState.Pressed:
                if (Input.GetMouseButtonUp(buttonID))
                {
                    result = PointerEventData.FramePressState.Released;
                }
                else
                {
                    result = PointerEventData.FramePressState.NotChanged;
                }
                break;
            case PointerEventData.FramePressState.Released:
                if (Input.GetMouseButtonDown(buttonID))
                {
                    result = PointerEventData.FramePressState.Pressed;
                }
                else
                {
                    result = PointerEventData.FramePressState.NotChanged;
                }
                break;
            default:
                break;
        }
        return result;
    }

}
