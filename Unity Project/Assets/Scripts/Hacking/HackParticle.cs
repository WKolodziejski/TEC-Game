using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackParticle : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hackable>())
            Destroy(gameObject);
    }

    public void FlyTo(Transform target)
    {
        transform.LookAt(target);
        GetComponent<Rigidbody2D>().AddForce(transform.forward * .1f);
    }

}
