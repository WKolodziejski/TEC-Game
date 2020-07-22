using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Bullet : MonoBehaviour
{

    public float speed = 100f;
    public float damage = 1f;
    public GameObject hit;
    public GameObject muzzle;

    protected Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;

        Destroy(Instantiate(muzzle, gameObject.transform.position, gameObject.transform.rotation), 1f);
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(tag + " -> " + collision.name);

        /*
         * BulletPlayer não contém Player
         * BulletEnemy não contém Enemy
         * Bullet____ != Bullet____
        */

        if (!tag.Contains(collision.tag) && !collision.tag.Contains("Bullet"))
        {
            Explode();

            if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
                collision.GetComponent<Character>().TakeDamage(damage);
        }
    }

    protected void Explode()
    {
        Destroy(Instantiate(hit, gameObject.transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }

}
