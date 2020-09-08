using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    void Update()
    {
        if (Input.GetButtonDown("Jump")){
            rb.AddForce(300*Vector3.up);
        }
    }
}
