using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerMissile : MonoBehaviour
{

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.right * 100f);
    }

    
    void Update()
    {
        if (transform.rotation.eulerAngles.z > -90)
            transform.rotation *= Quaternion.Euler(0, 0, -10 * Time.deltaTime);
    }

}
