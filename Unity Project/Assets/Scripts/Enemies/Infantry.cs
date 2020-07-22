using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Infantry : Character //TODO: pular, outros inimigos, mudar de facção, limpar as funções
{

    public Weapon weaponPrefab;

    protected Transform playerT;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected Weapon weapon;
    protected EnemySpawner enemySpawner;

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

    protected void setPlayerTransform()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();
    }

    protected void setSprite()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected void setAnimator()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected void setWeapon()
    {
        this.weapon = GetComponent<Weapon>();
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }

    protected void setEnemySpawner() //caso criada, adicionar apenas CameraData no infantry
    {
        this.enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

}
