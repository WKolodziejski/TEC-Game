using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class HackingController : HackingCharacter
public class HackingController : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb;

    public float speed = 10f;
    float horizontalMov;
    float verticalMov;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalMov = Input.GetAxis("Vertical");
        horizontalMov = Input.GetAxis("Horizontal");
    }

    void FixedUpdate(){
        transform.position = transform.position + new Vector3(horizontalMov * speed * Time.deltaTime, 
                                                            verticalMov * speed * Time.deltaTime,
                                                            0);
    }

    //Collider Test
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        //Destroy(gameObject);
    }*/

    /*void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        //if (collisionInfo.collider.tag == "Enemy")
            //character.takeDamage(1);
        //Debug.Log(collisionInfo.rigidbody);
        //Debug.Log(collisionInfo.transform);
    }*/
}
