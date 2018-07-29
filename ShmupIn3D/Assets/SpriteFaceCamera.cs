using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour {
    Camera theCam;
    public GameObject pseudoParent;
    public bool hasPseudoParent = true;
    public bool lockRotLikeCam = true;
    public bool lockToReticule = false;
    public bool projectileLock = false;
    Crosshair reticule;

    // Use this for initialization
    void Awake () {
        theCam = Camera.main;
        reticule = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        if (projectileLock)
        {
            Vector3 vecToLook;
            vecToLook = transform.position - new Vector3(reticule.pointToLook.x, 0.0f, reticule.pointToLook.z);
            float angToReticule = Vector3.SignedAngle(Vector3.left * 10000000000, vecToLook, Vector3.down);
            if (angToReticule >= 90.0f || angToReticule <= -90.0f)
                transform.eulerAngles = new Vector3(180.0f, 0.0f, -angToReticule);
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angToReticule);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockRotLikeCam)
        {
            Vector3 temp;
            temp = theCam.transform.eulerAngles;
            temp.z = gameObject.transform.eulerAngles.z;
            transform.eulerAngles = temp;
        }
        if (hasPseudoParent)
            transform.position = pseudoParent.transform.position;

        if (lockToReticule)
        {
            Vector3 vecToLook;
            vecToLook = transform.position - new Vector3(reticule.pointToLook.x, 0.0f, reticule.pointToLook.z);
            float angToReticule = Vector3.SignedAngle(Vector3.left * 10000000000,vecToLook, Vector3.down);
            //Debug.Log(angToReticule);
            if(angToReticule >= 90.0f || angToReticule <= -90.0f)
                transform.eulerAngles = new Vector3(180.0f, 0.0f, -angToReticule);
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angToReticule);
            }

        }
    }
}
