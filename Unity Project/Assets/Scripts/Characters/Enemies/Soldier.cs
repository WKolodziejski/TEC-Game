using System;
using UnityEngine;

public class Soldier : Enemy2D //TODO: usar AStar para o soldier?, condicionar melhor o pulo, talvez usar VectorDistance, usar variaveis static para padrozinar a classe, adicionar um pequeno fator de aleatoriedade na moveSpeed, no CheckIfMoved checar se o pulo falhou 
{

    public float moveCooldown = 0.5f;
    public float followRange = 5f;
    public float jumpForce = 15f;
    readonly float moveCheckRate = 0.125f;

    private float nextMove;
    private float moveCheck;
    private float prevPosition;
    private float lastJump;
    private bool grounded;
    private Vector3 desiredDir;
    private JumpCheck jCheck;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        jCheck = GetComponentInChildren<JumpCheck>();
        desiredDir = new Vector3(-movementSpeed * Time.fixedDeltaTime, 0f, 0f); //setDesiredDir();
        nextMove = moveCooldown; //setFirstMove();
        ResetMoveCheck();
        lastJump = Time.time;
    }

    void FixedUpdate()
    {
        if (GetTarget() != null) {
            Move();
            Attack();
        }

        Jump();
    }

    public override void Attack()
    {

        if (weapon.CanFire()) {

            if (GetTarget().position.x < transform.position.x && desiredDir.x > 0)//SetShootingDir(); //TODO: dar soco e talvez se afastar um pouco antes de poder atirar de novo
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

            animator.SetTrigger("shoot");
            animator.SetBool("Running", false);

            weapon.Fire(mainBarrel); //Fire();

            this.nextMove = moveCooldown; //UpdateNextMove();
        }
    }

    private void Move()
    {
        nextMove -= Time.fixedDeltaTime;

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

            moveCheck -= Time.fixedDeltaTime; // CheckIfMoved()
            if (moveCheck < 0)
            {
                if ((Math.Abs(prevPosition - transform.position.x) < Math.Abs(moveCheckRate * movementSpeed / 2))) 
                {
                    if (((this.transform.position.x > GetTarget().position.x) && desiredDir.x > 0) || ((this.transform.position.x < GetTarget().position.x) && desiredDir.x < 0))
                        ChangeDesiredDir();
                    else if (JumpCooldown()) //JumpOnGround()
                    {
                        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                        lastJump = Time.time;
                        animator.SetBool("jumping", true);
                        animator.SetBool("Running", false);
                        ResetMoveCheck();
                    }
                }
                else
                {
                    ResetMoveCheck();
                }
            }

            this.transform.position += desiredDir;

            animator.SetBool("Running", true);
        }
    }

    private void Jump()
    {
        if (grounded && JumpCooldown() && !jCheck.ground) //&& (this.transform.position.y - followRange/2 < GetTarget().position.y)) //TODO: aperfeiçoar o parametro de diferença de altura
        {
            if (((this.transform.position.x > GetTarget().position.x) && desiredDir.x > 0) || ((this.transform.position.x < GetTarget().position.x) && desiredDir.x < 0))
            {
                ChangeDesiredDir();
                lastJump = Time.time - 0.3f;
            }
            else
            {
                rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                lastJump = Time.time;
                animator.SetBool("jumping", true);
                animator.SetBool("Running", false);
            }
        }
    }

    private bool JumpCooldown()
    {
        return (Time.time - lastJump >= 0.6f);
    }

    private void ChangeDesiredDir()
    {
        desiredDir = -1*desiredDir;
        transform.Rotate(0f, 180f, 0f);
        ResetMoveCheck();
    }

    private void ResetMoveCheck()
    {
        this.moveCheck = moveCheckRate;
        this.prevPosition = transform.position.x;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.CompareTag("Ground")) || (collider.CompareTag("Platform")))
        {
            grounded = true;
            animator.SetBool("jumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Ground")) || (collider.CompareTag("Platform")))
        {
            grounded = false;
        }
    }

}

