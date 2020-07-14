using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingPlayer : HackingCharacter
{

    public float speed = 10f;
    public float turnSpeed = 5f;

    private Vector2 movement;
    private float angle = 90;

    void Update()
    {
        movement.y = Input.GetAxis("HackingVertical");
        movement.x = Input.GetAxis("HackingHorizontal");

        if (Input.GetAxis("HackingShootHorizontal") > 0)
        {
            if (Input.GetAxis("HackingShootVertical") > 0)
                angle = 45;

            else if (Input.GetAxis("HackingShootVertical") < 0)
                angle = 270;

            else
                angle = 0;
        }
        else if (Input.GetAxis("HackingShootHorizontal") < 0)
        {
            if (Input.GetAxis("HackingShootVertical") > 0)
                angle = 135;

            else if (Input.GetAxis("HackingShootVertical") < 0)
                angle = 225;

            else
                angle = 180;
        }
        else
        {
            if (Input.GetAxis("HackingShootVertical") > 0)
                angle = 90;

            else if (Input.GetAxis("HackingShootVertical") < 0)
                angle = 270;
        }
    }

    void FixedUpdate()
    {
        transform.position += Vector3.ClampMagnitude(movement, 1) * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Enemy"))
        {
            takeDamage(1);
        } 
    }

}
