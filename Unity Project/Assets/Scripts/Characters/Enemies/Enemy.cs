using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    private Character target;

    protected Transform GetTarget()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Character>();

        if (target != null)
            return target.IsDead() ? null : target.transform;
        else
            return null;
    }

}
