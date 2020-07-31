using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{

    private Character player;

    protected Transform GetTarget()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Character>();

        if (player != null)
            return player.IsDead() ? null : player.transform;
        else
            return null;
    }

}
