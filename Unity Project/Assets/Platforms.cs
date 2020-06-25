using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public Controller player;
    public float timeToFall = 0.3f;
    float waitTime = 0f;
    
    PlatformEffector2D effector2D;

    void Start(){
        effector2D = GetComponent<PlatformEffector2D>();
    }
    void Update()
    {
        if ((player.platform) && (Input.GetAxis("Vertical") < -0.8f) && (Input.GetAxis("Horizontal") < 0.2f) && (Input.GetAxis("Horizontal") > -0.2f)) {
           Debug.Log("waittime:"+ waitTime );
           if (waitTime > timeToFall) {
               StartCoroutine(player.Fall());
               waitTime = 0f;
           } else {
               waitTime += Time.deltaTime;
           }

        } 
    }


}
