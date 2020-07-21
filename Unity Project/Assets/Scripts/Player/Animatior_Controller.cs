using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatior_Controller : MonoBehaviour
{
    public Controller playerController;
    public Animator animator;
    public Transform player;
    float horizontal;
    float vertical;
    Vector3 scale;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (horizontal != 0f)
        {
            scale.y = horizontal > 0f ? 0 : 180;
        }

        player.localEulerAngles = scale;

        if (vertical != 0){
            if(vertical > 0f){
                animator.SetBool("up",true);
                animator.SetBool("down",false);
            } else {
                animator.SetBool("up",false);
                animator.SetBool("down",true);
            }
        } else {
            animator.SetBool("up",false);
            animator.SetBool("down",false);
        }

        animator.SetBool("hacking",playerController.hacking);
        animator.SetBool("dead",playerController.dead);
        animator.SetBool("jumping", !(playerController.grounded));
        animator.SetBool("moving",(Input.GetAxis("Horizontal")!= 0f));
    }
}
