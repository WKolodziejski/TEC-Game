using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : MonoBehaviour
{
    protected Transform playerT;
    protected SpriteRenderer sprite;
    protected Animator animator;
    protected GameObject graphics;

    protected void setGraphics()
    {
        graphics = transform.Find("Sprite").gameObject;
    }
    protected void setPlayerTransform()
    {
        playerT = GameObject.Find("Player").GetComponent<Transform>();
    }

    protected void setSprite()
    {
        sprite = graphics.GetComponent<SpriteRenderer>();
    }

    protected void setAnimator()
    {
        animator = graphics.GetComponent<Animator>();
    }
}
