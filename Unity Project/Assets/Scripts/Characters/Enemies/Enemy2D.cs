using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2D : Enemy //TODO: pular, outros inimigos, mudar de facção, 
{

    protected EnemySpawner enemySpawner;
    protected delegate void AttackAction();
    protected AttackAction attackAction;

    //esse cu não deixa usar InitializeComponents() em abstratct!!!

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

    protected void SetEnemySpawner() 
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    protected override void OnDie()
    {
        base.OnDie();
        enemySpawner.Remove(gameObject);
    }
}
