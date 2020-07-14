using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBoss : HackingCharacter
{

    public float rotationSpeed = 5f;
    public float movementSpeed = 1f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = FindObjectOfType<HackingPlayer>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }


    public void SetAgressive()
    {
        Vector2 lookDir = player.position - transform.position;

        float angle = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        rb.velocity = transform.right * movementSpeed;
    }

    public void SetRunaway()
    {
        Vector2 lookDir = player.position - transform.position;

        float angle = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        rb.velocity = -transform.right * movementSpeed;
    }

}
