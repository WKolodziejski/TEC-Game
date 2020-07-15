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

    /*void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject c = collision.collider.gameObject;

        if (!c.CompareTag(tag))
        {
            Explode();

            if (c.CompareTag("Enemy") && CompareTag("BulletPlayer") || c.CompareTag("Player") && CompareTag("BulletEnemy"))
                c.GetComponent<HackingCharacter>().TakeDamage(damage);
        }
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        /*
         * BulletPlayer não contém Player
         * BulletEnemy não contém Enemy
         * Bullet____ != Bullet____
        */

        if (!tag.Contains(collision.tag))
        {
            Explode();

            if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
                collision.GetComponent<HackingCharacter>().TakeDamage(damage);
        }
    }

    protected void Explode()
    {
        Destroy(Instantiate(hit, gameObject.transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }

}
