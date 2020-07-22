using System;
using UnityEngine;

public class Soldier : Enemy2D //usar variaveis static para padrozinar a classe, no CheckIfMoved checar se essa não é diareção que o player está e simplemente parar até que ele mude de lado ou pular
{
    float moveSpeed;
    float moveCooldown;
    float nextMove;
    float followRange;
    float moveCheck;
    readonly float moveCheckRate = 0.125f;
    float prevPosition;
    Vector3 desiredDir;

    void Start()
    {
        //classe pai
        setTarget();
        setEnemySpawner();
        SetAnimator();
        setWeapon();
        attackAction = CanAttack;

        //locais
        this.moveSpeed = 5f; //setMoveSpeed(5f);
        this.moveCooldown = 0.5f; //setMoveCooldown(0.5f);
        this.followRange = 5f; //setFollowRange(5f);
        desiredDir = new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f); //setDesiredDir();
        nextMove = moveCooldown; //setFirstMove();
        barrel = gameObject.transform.Find("Barrel F").transform;
        resetMoveCheck();
    }

    void Update()
    {
        if (target) {
            Move();
            attackAction();
        }
    }

    public override void Attack()
    {

        if (weapon.CanFire()) {

            if (target.position.x < transform.position.x && desiredDir.x > 0)//SetShootingDir(); //talvez se afastar um pouco antes de poder atirar de novo
            {
                ChangeDesiredDir();
            }
            else
            {
                if (target.position.x > transform.position.x && desiredDir.x < 0)
                {
                    ChangeDesiredDir();
                }
            }

            animator.SetBool("Shooting", true);
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);

            weapon.Fire(barrel); //Fire();

            this.nextMove = moveCooldown; //UpdateNextMove();
        }
    }

    private void Move()
    {
        nextMove -= Time.deltaTime;

        if (nextMove < 0) {

            if ((this.transform.position.x > target.position.x + followRange) && desiredDir.x > 0) //CheckFollowBack() CheckDesiredDir()
            {
                ChangeDesiredDir();
            }
            else
            {
                if ((this.transform.position.x < target.position.x - followRange) && desiredDir.x < 0) //CheckFollowBack()
                {
                    ChangeDesiredDir();
                }
            }

            moveCheck -= Time.deltaTime; // CheckIfMoved()
            if (moveCheck < 0)
            {

                if ((Math.Abs(prevPosition - transform.position.x) < Math.Abs(moveCheckRate * moveSpeed / 2))) 
                {
                    ChangeDesiredDir();
                }
                else
                {
                    resetMoveCheck();
                }
            }

            desiredDir.Set(-moveSpeed * Time.deltaTime, 0f, 0f); //getDesiredDir();
            this.transform.position += desiredDir;

            if (!animator.GetBool("Running"))
            {
                animator.SetBool("Shooting", false);
                animator.SetBool("Jumping", false);
                animator.SetBool("Running", true);
            }
        }
    }

    private void ChangeDesiredDir()
    {
        moveSpeed = -moveSpeed;
        transform.Rotate(0f, 180f, 0f);
        resetMoveCheck();
    }

    private void resetMoveCheck()
    {
        this.moveCheck = moveCheckRate;
        this.prevPosition = transform.position.x;
    }

    protected override void OnDie()
    {
        enemySpawner.Remove(gameObject);
        Destroy(gameObject);
    }

}

