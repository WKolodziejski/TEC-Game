using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{

    protected Transform barrel;

    private Transform target;

    protected Transform GetTarget()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        return target;
    }

}
