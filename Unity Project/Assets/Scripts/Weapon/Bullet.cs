using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float damage = 1f;
    public float ttl = 1f;
    public GameObject hit;
    public GameObject muzzle;

    protected Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;

        Destroy(Instantiate(muzzle, gameObject.transform.position, gameObject.transform.rotation), 1f);
        Destroy(gameObject, ttl);
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
            if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
            {
                Character c = collision.GetComponent<Character>();

                if (!c.IsDead())
                {
                    Explode();
                    c.TakeDamage(damage);
                }
            }
            else
            {
                Explode();
            }
        }
    }

    protected void Explode()
    {
        Destroy(Instantiate(hit, gameObject.transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }

}
