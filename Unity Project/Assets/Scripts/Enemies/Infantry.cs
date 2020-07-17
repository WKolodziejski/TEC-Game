using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Infantry : Character //TODO: alterar mudança de direção, atirar, pular, não se esfregar em paredes, outros inimigos, mudar lados, atirar fora da tela?
{
    protected Transform playerT;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected Weapon weapon;

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

    /*public abstract void TakeDamage(int damage);

    protected void Die()
    {
        Destroy(gameObject);
    }*/
}
