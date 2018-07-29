using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTip : MonoBehaviour {

    Camera MainCam;
    public Vector3 pointToLook;
    public GameObject gunTip;

    // Use this for initialization
    void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray CameraRay = MainCam.ScreenPointToRay(MainCam.WorldToScreenPoint(gunTip.transform.position));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(CameraRay, out rayLength))
        {

            pointToLook = CameraRay.GetPoint(rayLength - 1f);
            transform.position = CameraRay.GetPoint(rayLength - 0.5f);
            //Debug.DrawLine(CameraRay.origin, pointToLook, Color.blue);

        }
    }
}
