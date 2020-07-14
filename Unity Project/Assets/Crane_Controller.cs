﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane_Controller : MonoBehaviour
{
    public float rangeH = 5f;
    public float rangeV= 5f;

    public float X = 0;
    public float Y = 0;
    public float speedH = 1f;
    public float speedV = 1f;
    public Transform HAxis;
    public Transform wire;
    private float targetH;
    private float targetV;

    void Start(){
       
    }

    void FixedUpdate()
    {
        if (HAxis.localPosition.x <= targetH){
            X = X + Time.deltaTime * speedH;
            targetH = rangeH;
        }
        else {
            X = X - Time.deltaTime * speedH;
            targetH = 0f;
        }
        
        HAxis.localPosition = new Vector3(X, HAxis.localPosition.y, HAxis.localPosition.z);

        if (transform.localPosition.y >= targetV){
            Y = Y - Time.deltaTime * speedV;
            targetV = -rangeV;
        }
        else {
            Y = Y + Time.deltaTime * speedV;
            targetV = 0f;
        }
        
        wire.localPosition = new Vector3(wire.localPosition.x, 1 + transform.localPosition.y/2, wire.localPosition.z);
        wire.localScale = new Vector3(wire.localScale.x,-transform.localPosition.y + 0.05f, wire.localScale.z);
        transform.localPosition = new Vector3(transform.localPosition.x, Y, transform.localPosition.z);
    }

    private void OnTriggerStay2D(Collider2D collider){
        if (collider.tag == "Player"){
            collider.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if (collider.tag == "Player"){
            collider.transform.SetParent(null);
        }
    }
}
