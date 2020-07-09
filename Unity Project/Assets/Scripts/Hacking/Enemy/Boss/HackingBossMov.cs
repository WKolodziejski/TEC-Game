using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBossMov : MonoBehaviour
{
    public float movSpeed;

    private Transform tr;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = tr.right * movSpeed;
    }

    //TODO  changing speed during game
    //      Change direction on collision
    void Update()
    {
        
    }
}
