using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Soldier : Enemy2D //TODO: usar o rb para mover o soldier, condicionar melhor o pulo?, talvez usar VectorDistance em algo, no CheckIfMoved checar se o pulo falhou, melhorar CheckFollow para evitar que o inimigo fique a frente do alvo
{

    public float moveCooldown = 0.5f;
    public float followRange = 5f;
    public float jumpForce = 15f;
    public float punchDamage = 1f;
    readonly float moveCheckRate = 0.125f;

    private float nextMove;
    private float moveCheck;
    private float prevPosition;
    private float lastJump;
    private float lastPunch;
    private bool grounded;
    private Vector3 desiredDir;
    private JumpCheck jCheck;
    private bool insidePlayersCone;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        jCheck = GetComponentInChildren<JumpCheck>();
        movementSpeed += Random.Range(-movementSpeed * 0.05f, movementSpeed * 0.05f);
        desiredDir = new Vector3(-movementSpeed * Time.fixedDeltaTime, 0f, 0f); //setDesiredDir();
        nextMove = moveCooldown; //setFirstMove();
        ResetMoveCheck();
        lastJump = Time.time;
        lastPunch = Time.time;
    }

    void FixedUpdate()
    {
        if (GetTarget() != null)
        {
            Move();
            Attack();
        }
        else
            animator.SetBool("Running", false);

        Jump();
    }

    public override void Attack()
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) < 1f)
        {
            if (Time.time - lastPunch >= 2f) //Punch() 
            {
                lastPunch = Time.time;
                target.TakeDamage(punchDamage, false);
                animator.SetBool("Running", false);
                animator.SetTrigger("punch");
                this.nextMove = moveCooldown; //UpdateNextMove();
            }
        }
        else
        {
            if (weapon.CanFire())
            {

                if (GetTarget().position.x < transform.position.x && desiredDir.x > 0)//SetShootingDir(); //TODO: talvez se afastar um pouco antes de poder atirar de novo
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
                    else if (JumpCooldown()) //JumpOnGround() //TODO: verificar se falhou
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
        if (grounded && JumpCooldown() && !jCheck.ground)
        {
            Transform target = GetTarget();

            if (target != null)
            {            
                if (((this.transform.position.x > target.position.x) && desiredDir.x > 0) || ((this.transform.position.x < target.position.x) && desiredDir.x < 0))
                {
                    ChangeDesiredDir();
                    lastJump = Time.time - 0.3f;
                }
                else
                {
                    if (!insidePlayersCone)
                    {
                        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                        lastJump = Time.time;
                        animator.SetBool("jumping", true);
                        animator.SetBool("Running", false);
                    }
                }
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
            animator?.SetBool("jumping", false); //tava dando erro sem o null conditional
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Ground")) || (collider.CompareTag("Platform")))
        {
            grounded = false;
        }
    }

    public void InsideCone(bool inside)
    {
        insidePlayersCone = inside;
    }

}

