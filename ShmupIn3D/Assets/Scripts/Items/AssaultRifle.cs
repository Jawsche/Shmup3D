using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AssaultRifle : Weapon
{

    public Projectile bulletPrefab = new Projectile();
    public GameObject emitPoint;
    Rigidbody2D bulletPrefabRB;
    public float ProjectileSpeed = 400.0f;
    public bool playerUsed = false;

    // Use this for initialization
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        reticule = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();



        AudioSrc = GetComponent<AudioSource>();
        camShake = Camera.main.GetComponent<CameraShake>();
        bulletPrefabRB = bulletPrefab.GetComponent<Rigidbody2D>();

        if (transform.root.gameObject == thePlayer)
            playerUsed = true;

        if (playerUsed)
            lookTarget = reticule.pointToLook;
        else
            lookTarget = thePlayer.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        vecTovecToTrg = emitPoint.transform.position - lookTarget;
        LookAtMouse();

        if (transform.parent.transform.parent.gameObject.GetComponent<Character>().isDead)
            gameObject.SetActive(false);

    }

    void LookAtMouse()
    {

        transform.LookAt(new Vector3(reticule.pointToLook.x, transform.position.y, reticule.pointToLook.z));
    }

    void makeProjectile(Vector3 source, GameObject gun)
    {
        //Pass projectile the Weapon stats it needs
        bulletPrefab.Damage = Damage;
        bulletPrefab.projSpeed = ProjectileSpeed;
        bulletPrefab.concForce = concQuote;
        bulletPrefab.vecToReticule = vecTovecToTrg;

        Rigidbody2D RB;
        RB = Instantiate(bulletPrefabRB, source, emitPoint.transform.rotation) as Rigidbody2D;
    }

    public void Fire()
    {
        if (Time.time > nextAtk)
        {
            makeProjectile(emitPoint.transform.position, gameObject);
            nextAtk = Time.time + atkRate;
            camShake.Shake(camShakeAmt, camShakeLength);
            AudioSrc.Play();
        }
    }
}
