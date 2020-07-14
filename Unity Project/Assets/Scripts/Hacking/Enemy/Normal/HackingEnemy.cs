using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEnemy : HackingCharacter
{

    public float movSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 lookDir = player.position - transform.position;

        float angle = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        rb.velocity = transform.right * movSpeed;
    }

    /*void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        //TODO  get bullet damage (if it can be > 1)
        if (collisionInfo.collider.tag == "PlayerBullet")
            takeDamage(1);
    }*/
}
