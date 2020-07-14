using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFollow : Bullet
{

    private Transform target;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Vector3 deltaPosition = speed * dir * Time.deltaTime;
        rb.MovePosition(transform.position + deltaPosition);
    }

}
