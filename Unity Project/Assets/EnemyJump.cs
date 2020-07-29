using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public jumpcheck jCheck;
    public float jumpLock = 0.6f;
    public float jumpForce = 15f;
    private float lastJump;
    private bool grounded;

    void start()
    {
       /* rb = GetComponent<Rigidbody2D>();
        jCheck = GetComponent<jumpcheck>();*/
    }

    void FixedUpdate(){
        Jump();
    }
    void Jump()
    {
        if (grounded && JumpCooldown() && !jCheck.ground)
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
        if ((collider.tag == "Ground") || (collider.tag == "Platform"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.tag == "Ground") || (collider.tag == "Platform"))
        {
            grounded = false;
        }
    }
}
