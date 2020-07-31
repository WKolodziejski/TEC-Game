using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpcheck : MonoBehaviour
{

    public bool ground;
    private void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log(tag + "->" + collider.tag);

        if ((collider.tag == "Ground") || (collider.tag == "Platform"))
        {
            ground = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log(tag + "->" + collider.tag);

        if ((collider.tag == "Ground") || (collider.tag == "Platform"))
        {
            ground = false;
        }
    }

}
