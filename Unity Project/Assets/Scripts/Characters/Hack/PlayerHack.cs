using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHack : Character
{

    public float turnSpeed = 5f;
    public Transform barrel;

    private float angle = 90;
    private Vector2 movement;

    protected override void InitializeComponents()
    {
        //
    }

    void Update()
    {
        movement.y = Input.GetAxis("HackingVertical");
        movement.x = Input.GetAxis("HackingHorizontal");

        if (Input.GetAxis("HackingShootHorizontal") > 0f)
        {
            if (Input.GetAxis("HackingShootVertical") > 0f)
                angle = 45;

            else if (Input.GetAxis("HackingShootVertical") < 0f)
                angle = 315;

            else
                angle = 0;
        }
        else if (Input.GetAxis("HackingShootHorizontal") < 0f)
        {
            if (Input.GetAxis("HackingShootVertical") > 0f)
                angle = 135;

            else if (Input.GetAxis("HackingShootVertical") < 0f)
                angle = 225;

            else
                angle = 180;
        }
        else
        {
            if (Input.GetAxis("HackingShootVertical") > 0f)
                angle = 90;

            else if (Input.GetAxis("HackingShootVertical") < 0f)
                angle = 270;
        }

        if (Input.GetButton("Fire3"))
            weapon.Fire(barrel);
    }

    void FixedUpdate()
    {
        transform.position += Vector3.ClampMagnitude(movement, 1) * movementSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Collider2D c = collisionInfo.collider;

        if (c.CompareTag("Enemy"))
        {
            TakeDamage(1);
        } 
    }

}
