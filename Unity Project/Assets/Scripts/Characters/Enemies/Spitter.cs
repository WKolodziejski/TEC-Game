using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy2D
{

    public float jumpForce = 55f;

    private bool jumped;
    private bool jumping;

    public override void Attack()
    {
        if (weapon.CanFire())
            StartCoroutine(IFire());
    }

    protected override void InitializeComponents()
    {
        //throw new System.NotImplementedException();
    }

    void Update()
    {
        if (Vector2.Distance(GetTarget().position, transform.position) <= 10f && !jumped)
            StartCoroutine(IJump());

        if (jumped && ! jumping)
            Attack();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            jumping = false;
            animator.SetBool("jumping", jumping);
        }
    }

    private IEnumerator IJump()
    {
        jumping = true;
        jumped = true;

        rb.gravityScale = 3f;

        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        rb.AddForce(Vector2.right * -2f, ForceMode2D.Impulse);

        animator.SetBool("jumping", jumping);

        yield return new WaitForSeconds(0.4f);

        GetComponent<BoxCollider2D>().enabled = true;
    }
    
    private IEnumerator IFire()
    {
        animator.SetTrigger("spit");
        
        yield return new WaitForSeconds(0.7f);

        animator.ResetTrigger("spit");

        weapon.Fire(mainBarrel);
    }

}
