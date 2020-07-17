using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float hp;

    private Action onDie;

    public void SetOnDieListener(Action onDie)
    {
        this.onDie = onDie;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            if (onDie != null)
                onDie();
            else
                Debug.Log("null");
            Destroy(gameObject);
        }
    }

}
