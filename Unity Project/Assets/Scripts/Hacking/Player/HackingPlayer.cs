using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingPlayer : HackingCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        //hp = 1;
        //fireRate = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnDisable()
    {
        Debug.Log("lost");

        ReturnHack hackEnd = GetComponent<ReturnHack>();
        hackEnd.exitHack();
    }*/

    public void OnDestroy()
    {
        Debug.Log("lost");

        FindObjectOfType<HackReturnController>().Return(1f);
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")
        {
            takeDamage(1);
        } 
    }
}
