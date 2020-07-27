using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Character
{

    public BoxCollider2D playerCollider;
    public BoxCollider2D groundcheck;
    public float fallTime = 0.3f;
    public float jumpLock = 0.6f;
    public float jumpForce = 15f;

    private Vector3 scale;
    private float horizontal;
    private float vertical;
    private float lastJump;
    private bool grounded;
    private bool platform;
    private bool hacking;

    public Transform barrelFront;
    public Transform barrelUp;
    public Transform barrelDiagonalUp;
    public Transform barrelDiagonalDown;
    public Transform hand;

    protected override void InitializeComponents()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            hacking = true;

        if (Input.GetButtonUp("Fire2"))
            hacking = false;

        if (!hacking)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (platform)
                        StartCoroutine(Fall());
                }
                else
                {
                    Jump();
                }
            }

            if (Input.GetButton("Fire3"))
            {
                weapon.Fire(mainBarrel);
            }

            if (vertical > 0f)
            {
                animator.SetBool("up", true);
                animator.SetBool("down", false);

                if (horizontal == 0f)
                    mainBarrel = barrelUp;
                else
                    mainBarrel = barrelDiagonalUp;
            }
            else if (vertical < 0f)
            {
                animator.SetBool("up", false);
                animator.SetBool("down", true);

                if (horizontal == 0f)
                    mainBarrel = barrelFront;
                else
                    mainBarrel = barrelDiagonalDown;
            }
            else
            {
                mainBarrel = barrelFront;
                animator.SetBool("up", false);
                animator.SetBool("down", false);
            }
        }

        animator.SetBool("hacking", hacking);
        animator.SetBool("dead", IsDead());
        animator.SetBool("jumping", !grounded);
        animator.SetBool("moving", horizontal != 0f);
    }

    void FixedUpdate()
    {
        if (!hacking && !IsDead())
            transform.position += horizontal * Time.deltaTime * movementSpeed * Vector3.right;

        if (horizontal != 0f)
        {
            scale.y = horizontal > 0f ? 0 : 180;
        }

        transform.localEulerAngles = scale;
    }

    void Jump()
    {
        if (grounded && JumpCooldown())
        {
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            lastJump = Time.time;
        }
    }

    private bool JumpCooldown()
    {
        return (Time.time - lastJump >= jumpLock);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (JumpCooldown())
        {
            if (collider.tag == "Ground")
            {
                grounded = true;
            }
            if (collider.tag == "Platform")
            {
                platform = true;
                grounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Ground")
        {
            grounded = false;
        }
        if (collider.tag == "Platform")
        {
            platform = false;
            grounded = false;
        }
    }

    public IEnumerator Fall()
    {
        playerCollider.enabled = false;
        lastJump = Time.time;
        yield return new WaitForSeconds(fallTime);
        playerCollider.enabled = true;
    }

}
