using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFollow : MissileBase
{

    private Transform target;

    void FixedUpdate()
    {
        if (fired)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Vector3 deltaPosition = speed * dir * Time.deltaTime;
            rb.MovePosition(transform.position + deltaPosition);
        }
        else
        {
            transform.position = transform.parent.position;
        }
    }

    protected override void Fire()
    {
        base.Fire();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
