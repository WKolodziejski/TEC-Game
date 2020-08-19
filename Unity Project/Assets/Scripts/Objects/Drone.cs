using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : Character
{

    public GameObject explosion;
    public Drop drop;

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    protected override void InitializeComponents()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI) * 0.5f;

        transform.position = tempPos;
    }

    protected override void OnDie()
    {
        base.OnDie();

        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(drop, transform.position, Quaternion.identity);
    }

}
