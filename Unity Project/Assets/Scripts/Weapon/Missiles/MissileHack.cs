using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HackSceneReference;

public class MissileHack : Bullet
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);

            FindObjectOfType<HackEnterController>().Enter(collision.collider.gameObject.transform, EDifficulty.EASY);
        }
        else if(!collision.collider.CompareTag(tag))
        {
            Explode();
        }
    }

}
