using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HackSceneReference;

public class HackingBoss : HackingCharacter
{

    public float rotationSpeed = 5f;
    public Transform barrel1;
    public Transform barrel2;
    public Transform barrelS;
    public GameObject shield;
    public GameObject shieldExp;

    private NavMeshAgent agent;
    private Transform player;
    private Weapon weapon;
    private Action GetDestination;
    private Vector3 destination;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        weapon = GetComponent<Weapon>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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
        destination = player.position;
    }

    private void GetRunawayPath()
    {        
        if (Vector3.Distance(transform.position, player.position) < 5f)
            destination = transform.position + ((transform.position - player.position) * 2); //TESTAR ESSE 2
    }

    public void DisableShield()
    {
        shieldExp.SetActive(true);
        shield.SetActive(false);
    }

}
