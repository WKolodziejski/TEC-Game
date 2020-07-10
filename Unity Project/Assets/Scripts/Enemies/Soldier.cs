using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Soldier : Infantry
{
    int moveSpeed;
    float atkCooldown;
    float nextAtk;
    float nextMove;
    float followRange;
    Vector3 desiredDir;
    // Start is called before the first frame update
    void Start()
    {
        setPlayerTransform();
        setGraphics();
        setSprite();
        setAnimator();
        setMoveSpeed(3);
        setAtkCooldown(0.5f);
        setFollowRange(5f);
        iniDesiredDir();
        iniNextMove();
        setNextAtk(Time.time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nextAtk > Time.time)
        {
            if (nextMove < Time.time)
            {
                move();
            }
        }
        else {
            Attack();
        }
    }

    private void setMoveSpeed(int moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private float getMoveSpeed()
    {
        return moveSpeed;
    }

    private void setAtkCooldown(float atkCooldown)
    {
        this.atkCooldown = atkCooldown;
    }

    private void setNextAtk(float nextAtk)
    {
        this.nextAtk = nextAtk;
    }

    private void updateNextAtk()
    {
        this.nextAtk = Time.time + UnityEngine.Random.Range(atkCooldown, atkCooldown * 10);
    }

    private void updateNextMove()
    {
        this.nextMove = Time.time + atkCooldown;
    }

    private void setFollowRange(float followRange)
    {
        this.followRange = followRange;
    }

    private void iniDesiredDir()
    {
        desiredDir = new Vector3(-getMoveSpeed() * Time.deltaTime, 0f, 0f);
    }

    private void iniNextMove()
    {
        nextMove = Time.time + atkCooldown;
    }

    private bool checkFollowBack()
    {
        if (this.transform.position.x < playerT.position.x - followRange)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool checkFollow()
    {
        if (this.transform.position.x > playerT.position.x + followRange*2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void changeDesiredDir()
    {
        desiredDir = -1 * desiredDir;
        sprite.flipX = !sprite.flipX;
    }

    private void checkDesiredDir()
    {
        if (checkFollow() && desiredDir.x > 0)
        {
            changeDesiredDir();
        } else
        {
            if (checkFollowBack() && desiredDir.x < 0)
            {
                changeDesiredDir();
            }
        }
    }

    private void move()
    {
        //print("Weeeeee");
        checkSpriteMovingDir();
        checkDesiredDir();
        this.transform.position += desiredDir;

        if (!animator.GetBool("Running"))
        {
            animator.SetBool("Shooting", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("Running", true);
        }
    }

    private void checkSpriteMovingDir()
    {
        if (desiredDir.x < 0)
        {
            sprite.flipX = false;
        }   else
        {
            sprite.flipX = true;
        }
    }

    private void setShootingDir()
    {
        if (playerT.position.x < transform.position.x)
        {
            sprite.flipX = false;
        } else
        {
            sprite.flipX = true;
        }
    }

    private void Attack() 
    {
        //print("Pew Pew");

        setShootingDir();

        animator.SetBool("Shooting", true);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        updateNextAtk();
        updateNextMove();
    }
}

