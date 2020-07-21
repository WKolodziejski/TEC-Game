using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerMissile : MonoBehaviour
{

    public GameObject tail;
    public GameObject explosion;

    private Rigidbody2D rb;
    private bool fired;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (fired)
            transform.rotation *= Quaternion.Euler(0, 0, -10 * Time.deltaTime);
    }

    public void Fire()
    {
        if (!fired)
        {
            fired = true;
            tail.SetActive(true);
            rb.gravityScale = 0.1f;
            rb.AddForce(Vector3.right * 500f * (transform.parent.transform.position.magnitude >= 0 ? 1 : -1));
            transform.SetParent(null);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(tag + " -> " + collision.name);

        if (!collision.CompareTag("Grid"))
        {
            Destroy(Instantiate(explosion, gameObject.transform.position, Quaternion.identity), 2f);
            Destroy(gameObject);
        }
    }

}
