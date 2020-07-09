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

    void Start(){
        scale.Set(1f,1f,1f);
    }
    void Update(){
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        
        if (horizontal != 0f){
            if(horizontal > 0f)
                scale.y = 0;
            else
                scale.y = 180;
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


        animator.SetBool("jumping", !(playerController.grounded));
        animator.SetBool("moving",(Input.GetAxis("Horizontal")!= 0f));
    }
}
