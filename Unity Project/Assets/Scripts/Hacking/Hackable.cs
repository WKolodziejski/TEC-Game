using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HackSceneReference;

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

            portal = Instantiate(particleSuccess, transform.position, Quaternion.identity);

            FindObjectOfType<HackEnterController>().Enter(transform, difficulty);
        }
        else
        {
            action();
        }
    }

    void OnEnable()
    {
        if (isHacked)
        {
            Debug.Log(HackSceneReference.Instance.Won());

            Destroy(portal, 1f);

            //TODO: se for inimigo: matar
            //      se for objeto permitir acesso chamando action()
        }
    }

}
