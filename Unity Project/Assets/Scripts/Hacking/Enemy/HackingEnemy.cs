using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HackingEnemy : Character
{

    public Transform barrel;

    private Rigidbody2D rb;
    private NavMeshAgent agent;
    private Transform player;
    private Weapon weapon;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        weapon = GetComponent<Weapon>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 lookDir = player.position - transform.position;
            rb.rotation = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;

            agent.destination = player.position;
        }
    }

    public void Fire()
    {
        weapon.Fire(barrel);
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }

}
