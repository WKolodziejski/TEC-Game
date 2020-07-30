using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{

    private Player2D player;

    protected Transform GetTarget()
    {
        if (player == null)
            player = FindObjectOfType<Player2D>();

        if (player != null)
            return player.IsDead() ? null : player.transform;
        else
            return null;
    }

}
