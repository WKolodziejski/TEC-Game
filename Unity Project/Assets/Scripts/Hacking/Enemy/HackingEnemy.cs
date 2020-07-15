using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEnemy : HackingCharacter
{

    public float movSpeed = 2f;
    public Transform barrel;

    private Transform player;
    private Rigidbody2D rb;
    private Weapon weapon;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 lookDir = player.position - transform.position;

            float angle = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;

            rb.velocity = transform.right * movSpeed;
        }
    }

    void Update()
    {
        weapon.Fire(barrel);
    }

}
