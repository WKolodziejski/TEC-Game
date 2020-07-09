using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEnemyMov : MonoBehaviour
{
    public float movSpeed = 2f;
    
    private Rigidbody2D rb;
    private Transform tr;
    private Transform target;
    private Vector2 playerPos;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            playerPos = target.position;
    }

    void FixedUpdate()
    {
        Vector2 lookDir = playerPos - rb.position;
        
        float angle = - Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        rb.velocity = tr.right * movSpeed;
    }
}
