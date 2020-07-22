using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2D : Enemy //TODO: pular, outros inimigos, mudar de facção, 
{
    protected EnemySpawner enemySpawner;
    protected Animator animator;
    protected delegate void AttackAction();
    protected AttackAction attackAction;

    protected void CanAttack()
    {
        float[] boundries = enemySpawner.getCameraBoundries();
        if (transform.position.x > boundries[0] && transform.position.x < boundries[1] &&
            transform.position.y > boundries[2] && transform.position.y < boundries[3])
        {
            attackAction = Attack;
        }
    }

    public abstract void Attack();

    protected override void setTarget()
    {
        target = FindObjectOfType<Player>().transform;
    }

    protected void setEnemySpawner() 
    {
        this.enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    protected void SetAnimator()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
