using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float damage = 1f;
    public GameObject hit;
    public GameObject muzzle;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;

        Destroy(Instantiate(muzzle, gameObject.transform.position, gameObject.transform.rotation), 1f);
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("PlayerBullet"))
        {
            Destroy(Instantiate(hit, gameObject.transform.position, Quaternion.identity), 1f);
            Destroy(gameObject);
        }
        
    }

}
