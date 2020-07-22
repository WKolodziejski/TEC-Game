using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HackingEnemy : Enemy
{
    private Rigidbody2D rb;
    protected NavMeshAgent agent;

    void Start()
    {
        setTarget();
        setWeapon();
        barrel = gameObject.transform.Find("Barrel").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 lookDir = target.position - transform.position;
            rb.rotation = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;

            agent.destination = target.position;
        }
    }

    protected override void setTarget() //talvez funcione só com <Player>, nesse caso passe o código para a classe Enemy
    {
        target = FindObjectOfType<HackingPlayer>().transform;
    }

protected void setNavMash()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}
