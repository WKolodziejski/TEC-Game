using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private Lifebar lifebar;
    
    void Start()
    {
        lifebar = FindObjectOfType<Lifebar>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lifebar.SetCheckpoint(collision.bounds.center.x);
        }
    }

}
