using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBoss : HackingCharacter
{
    public float rotationSpeed;
    private Transform tr;
    
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        tr.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    /*void OnDisable()
    {
        Debug.Log("Won");

        ReturnHack hackEnd = GetComponent<ReturnHack>();
        hackEnd.exitHack();
    }*/

    private void OnDestroy()
    {
        Debug.Log("won");

        FindObjectOfType<HackReturnController>().Return(0);
    }

}
