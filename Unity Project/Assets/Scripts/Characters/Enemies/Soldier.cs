using System;
using UnityEngine;

public class Soldier : Enemy2D //usar variaveis static para padrozinar a classe, no CheckIfMoved checar se essa não é diareção que o player está e simplemente parar até que ele mude de lado ou pular
{

    public float moveCooldown = 0.5f;
    public float followRange = 5f;
    readonly float moveCheckRate = 0.125f;

    private float nextMove;
    private float moveCheck;
    private float prevPosition;
    private Vector3 desiredDir;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        desiredDir = new Vector3(-movementSpeed * Time.deltaTime, 0f, 0f); //setDesiredDir();
        nextMove = moveCooldown; //setFirstMove();
        resetMoveCheck();
    }

    void Update()
    {
        if (GetTarget() != null) {
            Move();
            Attack();
        }
    }

    public override void Attack()
    {

        if (weapon.CanFire()) {

            if (GetTarget().position.x < transform.position.x && desiredDir.x > 0)//SetShootingDir(); //talvez se afastar um pouco antes de poder atirar de novo
            {
                ChangeDesiredDir();
            }
            else
            {
                if (GetTarget().position.x > transform.position.x && desiredDir.x < 0)
                {
                    ChangeDesiredDir();
                }
            }

            animator.SetBool("Shooting", true);
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);

            weapon.Fire(mainBarrel); //Fire();

            this.nextMove = moveCooldown; //UpdateNextMove();
        }
    }

    private void Move()
    {
        nextMove -= Time.deltaTime;

        if (nextMove < 0) {

            if ((this.transform.position.x > GetTarget().position.x + followRange) && desiredDir.x > 0) //CheckFollowBack() CheckDesiredDir()
            {
                ChangeDesiredDir();
            }
            else
            {
                if ((this.transform.position.x < GetTarget().position.x - followRange) && desiredDir.x < 0) //CheckFollowBack()
                {
                    ChangeDesiredDir();
                }
            }

            moveCheck -= Time.deltaTime; // CheckIfMoved()
            if (moveCheck < 0)
            {

                if ((Math.Abs(prevPosition - transform.position.x) < Math.Abs(moveCheckRate * movementSpeed / 2))) 
                {
                    ChangeDesiredDir();
                }
                else
                {
                    resetMoveCheck();
                }
            }

            desiredDir.Set(-movementSpeed * Time.deltaTime, 0f, 0f); //getDesiredDir();
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
        movementSpeed = -movementSpeed; //Isso aqui tá bem gambiarra hein
        transform.Rotate(0f, 180f, 0f);
        resetMoveCheck();
    }

    private void resetMoveCheck()
    {
        this.moveCheck = moveCheckRate;
        this.prevPosition = transform.position.x;
    }

}

