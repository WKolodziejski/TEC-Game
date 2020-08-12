using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{

    public float targetDistance = 20f;

    protected Character target;

    protected Transform GetTarget()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Character>();

        if (target != null)
            if (!target.IsDead())
                if (Vector2.Distance(target.transform.position, transform.position) <= targetDistance)
                    return target.transform;

        return null;
    }

}
