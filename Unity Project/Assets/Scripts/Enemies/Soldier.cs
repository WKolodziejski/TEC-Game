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
        setPrevPosition();
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
            //CheckSpriteMovingDir();
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

    /*private bool toTheLeft()
    {
        if (this.transform.position.x > playerT.position.x )
        {
            return true;
        } else
        {
            return false;
        }
    }*/

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
            resetMoveCheck();

            if ((Math.Abs(prevPosition - transform.position.x) < moveCheck*moveSpeed/2)){
                ChangeDesiredDir(); //resetmovecheck redundante, mas necessario
            }
        }
    }

    private void ChangeDesiredDir()
    {
        desiredDir = -1 * desiredDir;
        transform.Rotate(0f, 180f, 0f);
        resetMoveCheck();
        setPrevPosition();
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

    /*private void CheckSpriteMovingDir()
    {
        if (desiredDir.x < 0)
        {
            transform.rotation.Set(0f, 0f, 0f, (float)Space.World);
        }   else
        {
            transform.rotation.Set(0f, 180f, 0f, (float)Space.World);
        }
    }*/

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

    /*public override void TakeDamage(int damage)
    {
        Die();
    }*/

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

    private void resetMoveCheck() //adicionar o setprevposition seria intuitivo, mas causaria problemas no checkifmoved
    {
        this.moveCheck = 0.125f;
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

    private void setPrevPosition()
    {
        this.prevPosition = transform.position.x;
    }

}

