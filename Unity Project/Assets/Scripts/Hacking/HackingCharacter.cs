using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingCharacter : MonoBehaviour
{
    public int hp;
    public float fireRate; // shoots/sec

    private Action onDie;

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void takeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
            die();
    }

    public void die() {
        //gameObject.SetActive(false);
        Destroy(gameObject);

        onDie();
    }

}
