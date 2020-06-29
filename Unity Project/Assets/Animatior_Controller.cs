using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatior_Controller : MonoBehaviour
{
    public Controller playerController;
    public Animator animator;
    public Transform player;
    float horizontal;
    Vector3 scale;

    void Start(){
        scale.Set(1f,1f,1f);
    }
    void Update(){
        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        
        if (horizontal != 0f){
            if(horizontal > 0f)
                scale.x = 1f;
            else
                scale.x = -1f;
        }
        player.localScale = scale;

        animator.SetBool("jumping", !(playerController.grounded));
        animator.SetBool("moving",(Input.GetAxis("Horizontal")!= 0f));
    }
}
