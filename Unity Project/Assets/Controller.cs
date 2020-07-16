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

    //Weapon
    public Transform barrelFront;
    public Transform barrelUp;
    public Transform barrelDiagonalUp;
    public Transform barrelDiagonalDown;

    private Transform barrel;
    private Weapon weapon;

    PolygonCollider2D playerCollider;
    public bool grounded = false;
    public bool platform = false;

    //Hack variables
    public HackInterface hackInterface;

    private float startTime;
    private float countTime;
    private bool held;


    void Start()
    {
        /*tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        groundcheck = GetComponent<BoxCollider2D>();*/
        playerCollider = GetComponent<PolygonCollider2D>();
        weapon = GetComponent<Weapon>();

        hackInterface.SetPlayerPosition(transform);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                if (platform)
                {
                    StartCoroutine(Fall());
                }
            }
            else
            {
                Jump();
            }
            
        }

        Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Fire2") && !held)
        {
            startTime = Time.time;
            countTime = startTime;

            hackInterface.StartHacking();
        }

        if (Input.GetButton("Fire2") && !held)
        {
            countTime += Time.deltaTime;

            held = hackInterface.SetProgress(countTime, startTime);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            held = false;
            hackInterface.CancelHacking();
        }

        if (Input.GetButton("Fire3"))
        {
            weapon.Fire(barrel);
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0.1f)
        {
            if ((horizontal <= 0.1f)&&(horizontal >= -0.1f))
                barrel = barrelUp;
            else
                barrel = barrelDiagonalUp;
        }
        else if (vertical < -0.1f)
        {
            if ((horizontal <= 0.1f)&&(horizontal >= -0.1f))
                barrel = barrelFront;
            else
                barrel = barrelDiagonalDown;
        }
        else
            barrel = barrelFront;
    }

    void FixedUpdate() {
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
