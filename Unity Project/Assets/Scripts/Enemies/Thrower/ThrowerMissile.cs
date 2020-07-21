using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerMissile : MonoBehaviour
{

    public GameObject tail;

    private Rigidbody2D rb;
    private bool fired;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (fired)
            transform.rotation *= Quaternion.Euler(0, 0, -10 * Time.deltaTime);
    }

    public void Fire()
    {
        fired = true;
        tail.SetActive(true);
        rb.gravityScale = 0.1f;
        rb.AddForce(Vector3.right * 30f);
    }

}
