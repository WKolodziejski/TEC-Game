using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HackSceneReference;

[RequireComponent(typeof(Collider2D))]
public class Hackable : MonoBehaviour
{

    public EDifficulty difficulty;
    public GameObject particleSuccess;

    private bool isHacked;
    private GameObject portal;
    protected Action action;

    public void Hack()
    {
        if (!isHacked)
        {
            isHacked = true;

            if (particleSuccess != null)
                portal = Instantiate(particleSuccess, transform.position, Quaternion.identity, transform);

            FindObjectOfType<HackSceneReference>().Enter(transform, difficulty);
        }
        else
        {
            action?.Invoke();
        }
    }

    void OnEnable()
    {
        if (isHacked)
        {
            Destroy(portal, 1f);

            //TODO: se for inimigo: matar
            //      se for objeto permitir acesso chamando action()
        }
    }

    public void SetAction(Action action)
    {
        this.action = action;
    }

}
