using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingCharacter : MonoBehaviour
{
    public float hp;
    public float fireRate; // shoots/sec

    private Action onDie;

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void takeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            onDie();
            Destroy(gameObject);
        }
            
    }

}
