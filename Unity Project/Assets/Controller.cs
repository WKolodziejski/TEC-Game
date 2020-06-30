using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 15;
    public BoxCollider2D groundcheck;
    public Transform tr;
    public Rigidbody2D rb;
    public float horizontalMov;
    float lastJump;
    public float fallTime = 0.3f;
    public float jumpLock = 0.6f;

    PolygonCollider2D playerCollider;
    public bool grounded = false;
    public bool platform = false;
    void Start()
    {
        /*tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        groundcheck = GetComponent<BoxCollider2D>();*/
        playerCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")){
            Jump();
        }
        Move(Input.GetAxis("Horizontal"));
    }

    void FixedUpdate(){
        transform.position += horizontalMov * Time.deltaTime * speed * Vector3.right;
    }

    void Jump(){
        if (grounded && JumpCooldown()){
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            lastJump = Time.time;
        }
    }

    private bool JumpCooldown()
    {
        return (Time.time - lastJump >= jumpLock);
    }

    void Move(float input){
        horizontalMov = input;  
    }

    private void OnTriggerStay2D(Collider2D collider){
       if(JumpCooldown()){
            if (collider.tag == "Ground"){
                grounded = true;
            }

            if (collider.tag == "Platform"){
                platform = true;
                grounded = true;
            }
       }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if (collider.tag == "Ground"){
            grounded = false;
        }

        if (collider.tag == "Platform"){
            platform = false;
            grounded = false;
        }

    }
    
    public IEnumerator Fall(){
        playerCollider.enabled = false;
        lastJump = Time.time;
        yield return new WaitForSeconds(fallTime);
        playerCollider.enabled = true;
    }
}
