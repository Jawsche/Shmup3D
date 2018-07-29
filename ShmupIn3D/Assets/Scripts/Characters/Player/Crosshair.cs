using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Crosshair : MonoBehaviour {

	Camera MainCam;
    public Vector3 pointToLook;

	// Use this for initialization
	void Start () {
		MainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        Ray CameraRay = MainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(CameraRay, out rayLength)) {

            pointToLook = CameraRay.GetPoint(rayLength - 0.5f);
            transform.position = CameraRay.GetPoint(rayLength - 0.5f);
        }
	}
		
}
