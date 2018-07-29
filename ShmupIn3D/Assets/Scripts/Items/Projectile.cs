using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float Damage;
    public float projSpeed;
    public float concForce;
    public Vector3 vecToReticule;
    public GameObject hitEffect;


    Rigidbody m_rigidBody;
    

    private void Start()
    {
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_rigidBody.AddForce(vecToReticule.normalized * projSpeed * Time.deltaTime );
        //Debug.Log(vecToReticule.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<Character>().isDead==false)
                collision.gameObject.GetComponent<Character>().TakeDamage(Damage);

        }
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
            collision.gameObject.GetComponent<Rigidbody>().AddForce(vecToReticule.normalized * concForce * -500.0f);

        Instantiate(hitEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
