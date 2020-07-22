using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Player2D
{
    float horizontal; //animation_controller
    float vertical;
    Vector3 scale;

    public float jumpForce = 15;
    public BoxCollider2D groundcheck;
    public Rigidbody2D rb;
    public float horizontalMov;
    float lastJump;
    public float fallTime = 0.3f;
    public float jumpLock = 0.6f;

    //Weapon
    public Transform barrelFront;
    public Transform barrelUp;
    public Transform barrelDiagonalUp;
    public Transform barrelDiagonalDown;
    public Transform hand;

    private Transform barrel;

    public BoxCollider2D playerCollider;
    public bool grounded = false;
    public bool platform = false;
    public bool hacking = false;
    public bool dead = false;

    void Start()
    {
        /*tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        groundcheck = GetComponent<BoxCollider2D>();*/
        SetAnimator();
        setWeapon();
    }

    void Update()
    {
        if (!hacking || dead) //animation_controller
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        if (!dead) //controller
        {
            if (Input.GetButtonDown("Fire2"))
            {
                hacking = true;
            }

            if (Input.GetButtonUp("Fire2"))
            {
                hacking = false;
            }

            if (!hacking)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    if (Input.GetAxis("Vertical") < 0)
                    {
                        if (platform)
                        {
                            StartCoroutine(Fall());
                        }
                    }
                    else
                    {
                        Jump();
                    }

                }

                Move(Input.GetAxis("Horizontal"));

                if (Input.GetButton("Fire3"))
                {
                    weapon.Fire(barrel);
                }

                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");

                if (vertical > 0f)
                {
                    if (horizontal == 0f)
                        barrel = barrelUp;
                    else
                        barrel = barrelDiagonalUp;
                }
                else if (vertical < 0f)
                {
                    if (horizontal == 0f)
                        barrel = barrelFront;
                    else
                        barrel = barrelDiagonalDown;
                }
                else
                    barrel = barrelFront;
            }
        }
    }

    void FixedUpdate()
    {
        if (!hacking && !dead) //controller
            transform.position += horizontalMov * Time.deltaTime * speed * Vector3.right;

        if (horizontal != 0f) //animation_controller
        {
            scale.y = horizontal > 0f ? 0 : 180;
        }

        transform.localEulerAngles = scale;

        if (vertical != 0)
        {
            if (vertical > 0f)
            {
                animator.SetBool("up", true);
                animator.SetBool("down", false);
            }
            else
            {
                animator.SetBool("up", false);
                animator.SetBool("down", true);
            }
        }
        else
        {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }

        animator.SetBool("hacking", hacking);
        animator.SetBool("dead", dead);
        animator.SetBool("jumping", !(grounded));
        animator.SetBool("moving", (Input.GetAxis("Horizontal") != 0f));

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

    void Move(float input)
    {
        horizontalMov = input;
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

    protected override void OnDie()
    {
        dead = true;
        Destroy(gameObject, 1f);
    }

}
