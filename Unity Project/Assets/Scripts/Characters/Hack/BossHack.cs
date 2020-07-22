using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HackSceneReference;

public class BossHack : EnemyHack
{

    public GameObject shield;
    public GameObject shieldExp;

    public float rotationSpeed = 5f;
    public Transform barrel1;
    public Transform barrel2;
    public Transform barrelS;
    
    private Action GetDestination;
    private Vector3 destination;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        GetDestination += GetRunawayPath;

        if (HackSceneReference.Instance.GetDifficulty() == EDifficulty.EASY)
            shield.SetActive(false);
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        GetDestination();
        agent.SetDestination(destination);
    }

    void Update()
    {
        weapon.Fire(barrel1);
        //...

        
    }
    
    public void SetAgressive()
    {
        GetDestination += GetAgressivePath;
    }

    private void GetAgressivePath()
    {
        destination = GetTarget().position;
    }

    private void GetRunawayPath()
    {        
        if (Vector3.Distance(transform.position, GetTarget().position) < 5f)
            destination = transform.position + ((transform.position - GetTarget().position) * 2); //TESTAR ESSE 2
    }

    public void DisableShield()
    {
        if (HackSceneReference.Instance.GetDifficulty() != EDifficulty.EASY)
            shieldExp.SetActive(true);

        shield.SetActive(false);
    }

}
