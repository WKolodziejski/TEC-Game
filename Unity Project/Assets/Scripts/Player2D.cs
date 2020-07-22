using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player2D : Player
{
    protected Animator animator;

    protected void SetAnimator()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
