using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : MonoBehaviour
{

    public float delay;
    public float damage;
    public float speed;
    public GameObject explosion;
    public GameObject muzzle;

    protected Rigidbody2D rb;
    protected bool fired;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(IFire());
        StartCoroutine(IExplode());
    }

    void FixedUpdate()
    {
        if (!fired)
        {
            transform.position = transform.parent.position;
        }
    }

    private IEnumerator IFire()
    {
        yield return new WaitForSeconds(delay);
        Fire();
    }

    protected virtual void Fire()
    {
        Destroy(Instantiate(muzzle, transform.parent.transform), 1f);
        rb.velocity = transform.forward * speed;

        fired = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Missile"))
            Explode();
    }

    protected void Explode()
    {
        Destroy(gameObject);
        Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 1f);
    }

    private IEnumerator IExplode()
    {
        yield return new WaitForSeconds(delay + 2f);
        Explode();
    }

}
