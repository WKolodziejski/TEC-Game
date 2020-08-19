﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2D : Character
{

    public BoxCollider2D playerCollider;
    public BoxCollider2D groundcheck;
    public PolygonCollider2D enemyCone;
    public float fallTime = 0.3f;
    public float jumpLock = 0.6f;
    public float jumpForce = 15f;

    private Vector3 scale;
    private float horizontal;
    private float vertical;
    private float lastJump;
    public bool grounded;
    private bool platform;
    private bool hacking;
    private bool lying;
    private bool controlIsEnabled = true;

    public Transform barrelFront;
    public Transform barrelUp;
    public Transform barrelDiagonalUp;
    public Transform barrelDiagonalDown;
    public Transform barrelLying;
    public Transform hand;

    public PolygonCollider2D lyingCollider;
    public PolygonCollider2D standingCollider;

    public AudioSource audioDamage;
    public AudioSource audioCritical;
    public AudioSource audioSpawn;
    public AudioSource audioDie;
 
    private Joystick joystick;
    private TouchHandler touchButtons;


    protected override void InitializeComponents()
    {
        //assistant = GameObject.FindObjectOfType<AIAssistant>();
        joystick = GameObject.Find("TouchStick").GetComponent<VariableJoystick>();
        touchButtons = GameObject.Find("Touch").GetComponent<TouchHandler>();
        SetEnabled(true);
        audioSpawn.Play();
    }

    void Update()
    {
        if (GameController.isPaused)
            return;

        if (controlIsEnabled)
        {
            lying = false;

            if (Input.GetButtonDown("Fire2") || touchButtons.hack)
                hacking = true;

            if (Input.GetButtonUp("Fire2")|| !touchButtons.hack)
                hacking = false;

            if (!hacking)
            {
                horizontal = Input.GetAxis("Horizontal") + joystick.Horizontal;
                vertical = Input.GetAxis("Vertical") + joystick.Vertical;

                if (Input.GetButtonDown("Jump") || touchButtons.jump)
                {
                    if (vertical < 0)
                    {
                        if (platform)
                            StartCoroutine(Fall());
                    }
                    else
                    {
                        Jump();
                    }
                }

                if (Input.GetButton("Fire3") || touchButtons.fire)
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
                    {
                        lying = true;
                        mainBarrel = barrelLying;
                    }
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

            lyingCollider.enabled = lying;
            standingCollider.enabled = !lying;
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
            if (collider.CompareTag("Ground"))
            {
                grounded = true;
            }
            if (collider.CompareTag("Platform"))
            {
                platform = true;
                grounded = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Soldier>()?.InsideCone(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            grounded = false;
        }
        if (collider.CompareTag("Platform"))
        {
            platform = false;
            grounded = false;
        }
        if (collider.CompareTag("Enemy"))
            collider.GetComponent<Soldier>()?.InsideCone(false);
    }

    public IEnumerator Fall()
    {
        playerCollider.enabled = false;
        lastJump = Time.time;
        yield return new WaitForSeconds(fallTime);
        playerCollider.enabled = true;
    }

    protected override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        FindObjectOfType<HackInterface>().CancelHacking();
        hacking = false;

        if (hp >= 2)
            audioDamage.Play();
        else
            audioCritical.Play();
    }

    protected override void OnDie()
    {
        base.OnDie();

        audioDie.Play();
    }

    public void Walk(float direction)
    {
        horizontal = direction;
    }

    public void DisableControls()
    {
        controlIsEnabled = false;
        horizontal = 0f;
    }

    public void EnableControls()
    {
        controlIsEnabled = true;
        horizontal = 0f;
    }

    public void AddLife(float life)
    {
        float ohp = hp;
        float tmp = hp + life;

        hp = tmp <= maxHP ? tmp : maxHP;

        if (damagePopup != null)
            Instantiate(damagePopup, transform.position, Quaternion.identity).Hit(hp - ohp);
    }

 

    //private AIAssistant assistant;

    /*public override void SetEnabled(bool enabled)
    {
        if (!IsDead())
        {
            this.enabled = enabled;
            assistant.EnabledAI(this, enabled);
        }
    }*/

    /* private void OnDestroy()
    {
        assistant.EnabledAI(this, false);
    }*/
}
