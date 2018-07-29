using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : Character {

	
    protected bool isRunning = false;
	float input_H;
	float input_V;
    public Vector3 vecToReticule;
    public Crosshair reticule;
    



    // Use this for initialization
    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        //m_SpriteRend = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider>();
        //m_Animator = GetComponent<Animator>();
        m_AudioSrc = GetComponent<AudioSource>();
        isFriendly = true;
        reticule = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
    }


    // Update is called once per frame
    new void Update () {
        vecToReticule = gameObject.transform.position - reticule.transform.position;
        if (!isDead)
        {
            base.Update();
            if (!isDead)
                Move();
            Animation();
        }
        LookAtMouse();
    }

	void Animation(){
		m_Animator.SetFloat ("HorizSpeed", input_H);
		m_Animator.SetFloat ("VertSpeed", input_V);
        m_Animator.SetBool("IsRunning", isRunning);
        m_Animator.SetBool("isDead", isDead);
    }
    void Move()
    {
        input_H = CrossPlatformInputManager.GetAxis("Horizontal");
        input_V = CrossPlatformInputManager.GetAxis("Vertical");
        if (input_H != 0.0f)
        {
            m_RigidBody.AddForce(Vector3.right * input_H * speed * Time.deltaTime * 500.0f);
            isRunning = true;
        }
        if (input_V != 0.0f)
        {
            m_RigidBody.AddForce(Vector3.forward * input_V * speed * Time.deltaTime * 500.0f);
            isRunning = true;
        }
        if (input_V == 0.0f && input_H == 0.0f)
            isRunning = false;
    }
    void LookAtMouse()
    {
        transform.LookAt(new Vector3(reticule.pointToLook.x, transform.position.y, reticule.pointToLook.z));

        float angToPlayer = Vector3.SignedAngle(Vector3.left, reticule.pointToLook, Vector3.back);
        if (angToPlayer >= 90.0f || angToPlayer <= -90.0f)
        {
            m_SpriteRend.flipX = false;
        }
        else
        {
            m_SpriteRend.flipX = true;
        }
    }
}