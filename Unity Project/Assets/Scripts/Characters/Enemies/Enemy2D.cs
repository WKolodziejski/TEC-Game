using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2D : Enemy //TODO: pular, outros inimigos, mudar de facção, 
{

    protected EnemySpawner enemySpawner;
    //protected delegate void AttackAction();
    //protected AttackAction attackAction;

    //esse cu não deixa usar InitializeComponents() em abstratct!!!

    //CHAMAR BASE NOS FILHOS
    protected override void InitializeComponents()
    {
        //throw new System.NotImplementedException();
    }

    protected bool CanAttack()
    {
        float[] boundries = enemySpawner.getCameraBoundries();
        return transform.position.x > boundries[0] && transform.position.x < boundries[1] &&
            transform.position.y > boundries[2] && transform.position.y < boundries[3] ;
    }

    protected void LookAtTarget()
    {
        if (GetTarget() != null)
            transform.rotation = GetTargetRotation();
    }

    protected Quaternion GetTargetRotation()
    {
        if (GetTarget() != null)
            return transform.position.x < GetTarget().position.x ? Quaternion.identity : Quaternion.Euler(0f, 180f, 0f);
        else
            return Quaternion.identity;
    }

    protected int GetTargetMagnitude()
    {
        return GetTargetRotation() == Quaternion.identity ? 1 : -1;
    }

    public abstract void Attack();

    protected void SetEnemySpawner() 
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    protected override void OnDie()
    {
        base.OnDie();

        if (enemySpawner != null)
            enemySpawner.Remove(gameObject);
    }

}
