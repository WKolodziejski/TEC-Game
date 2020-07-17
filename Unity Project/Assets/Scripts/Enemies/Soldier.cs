using System;
using UnityEngine;

public class Soldier : Infantry
{
    public Transform barrel;
    float moveSpeed;
    float atkCooldown;
    float moveCooldown;
    float nextAtk;
    float nextMove;
    float followRange;
    float moveCheck;
    float moveCheckRate = 0.125f;
    float prevPosition;
    Vector3 desiredDir;

    void Start()
    {
        setPlayerTransform();
        setSprite();
        setAnimator();
        setMoveSpeed(1.5f);
        setAtkCooldown(0.5f); //se por em 0.5 fica levemente bugado
        setMoveCooldown(0.5f);
        setFollowRange(5f);
        setWeapon();
        setDesiredDir();
        setFirstMove();
        setFirstAtk();
        resetMoveCheck();
    }

    void Update()
    {
        Move();   
        Attack();
    }

    private void Attack()
    {
        nextAtk -= Time.deltaTime;

        if (nextAtk < 0) { 
            SetShootingDir(); //talvez se afastar um pouco antes de poder atirar de novo

            animator.SetBool("Shooting", true);
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);

            Fire();

            UpdateNextAtk();
            UpdateNextMove();
        }
    }

    private void Move()
    {
        nextMove -= Time.deltaTime;

        if (nextMove < 0) { 
            CheckDesiredDir();
            CheckIfMoved();

            this.transform.position += desiredDir;

            if (!animator.GetBool("Running"))
            {
                animator.SetBool("Shooting", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Running", true);
            }
        }
    }

    private void CheckDesiredDir()
    {

        if (CheckFollow() && desiredDir.x > 0)
        {
            ChangeDesiredDir();
        } else
        {
            if (CheckFollowBack() && desiredDir.x < 0)
            {
                ChangeDesiredDir();
            }
        }
    }

    private void CheckIfMoved()
    {
        moveCheck -= Time.deltaTime;
        if (moveCheck < 0) {

            if ((Math.Abs(prevPosition - transform.position.x) < moveCheckRate*moveSpeed/2)){
                ChangeDesiredDir();
            } else {
                resetMoveCheck();
            }
        }
    }

    private void ChangeDesiredDir()
    {
        desiredDir = -1 * desiredDir;
        transform.Rotate(0f, 180f, 0f);
        resetMoveCheck();
    }

    private bool CheckFollowBack()
    {
        if (this.transform.position.x < playerT.position.x - followRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckFollow()
    {
        if (this.transform.position.x > playerT.position.x + followRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetShootingDir()
    {
        if (playerT.position.x < transform.position.x && desiredDir.x > 0)
        {
            ChangeDesiredDir();
        } else
        {
            if (playerT.position.x > transform.position.x && desiredDir.x < 0) {
                ChangeDesiredDir();
            }
        }
    }

    private void Fire()
    {
        weapon.Fire(barrel);
    }

    private void setMoveSpeed(float moveSpeed)
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

    private void setMoveCooldown(float moveCooldown)
    {
        this.moveCooldown = moveCooldown;
    }


    private void UpdateNextAtk()
    {
        this.nextAtk = UnityEngine.Random.Range(atkCooldown, atkCooldown * 10);
    }

    private void UpdateNextMove()
    {
        this.nextMove = moveCooldown;
    }

    private void resetMoveCheck()
    {
        this.moveCheck = moveCheckRate;
        this.prevPosition = transform.position.x;
    }

    private void setFollowRange(float followRange)
    {
        this.followRange = followRange;
    }

    private void setDesiredDir()
    {
        desiredDir = new Vector3(-getMoveSpeed() * Time.deltaTime, 0f, 0f);
    }

    private void setFirstMove()
    {
        nextMove = atkCooldown;
    }

    private void setFirstAtk()
    {
        this.nextAtk = atkCooldown;
    }

}

