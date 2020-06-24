using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public Transform tr;
    public Rigidbody2D rb;
    public float speed = 30f;
    public float jumpForce = 300f;
    float horizontalMov;
    bool jump;
    public bool grounded = false;
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetButtonDown("Jump");

        if (jump && grounded){
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jump = false;
        }
        horizontalMov = Input.GetAxis("Horizontal");

        
    }

    void FixedUpdate(){
        transform.position += horizontalMov * Time.deltaTime * speed * Vector3.right;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.tag == "Ground"){
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.collider.tag == "Ground"){
            grounded = false;
        }
    }
}
