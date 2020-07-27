using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{

    private Transform target;
    private Player2D player;

    protected Transform GetTarget()
    {
        if (target == null)
        {
            player = FindObjectOfType<Player2D>();

            if (player == null)
                target = null;
            else
                target = player.IsDead() ? null : player.transform;
        }

        return target;
    }

}
