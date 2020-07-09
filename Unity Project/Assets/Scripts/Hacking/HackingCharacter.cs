using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingCharacter : MonoBehaviour
{
    public int hp;
    public float fireRate; // shoots/sec

    //protected GameObject character;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
            die();
    }

    public void die() {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
