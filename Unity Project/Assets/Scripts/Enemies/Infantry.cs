using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : MonoBehaviour
{
    protected Transform playerT;
    protected SpriteRenderer sprite;
    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void setPlayerTransform()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();
    }

    protected void setSprite()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    protected void setAnimator()
    {
        animator = GetComponent<Animator>();
    }
}
