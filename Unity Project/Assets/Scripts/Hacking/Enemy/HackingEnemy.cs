using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HackingEnemy : HackingCharacter
{

    public Transform barrel;

    private NavMeshAgent agent;
    private Transform player;
    private Weapon weapon;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        weapon = GetComponent<Weapon>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            agent.destination = player.position;
        }
    }

    public void Fire()
    {
        weapon.Fire(barrel);
    }

}
