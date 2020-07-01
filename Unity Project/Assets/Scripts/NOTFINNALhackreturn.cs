using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTFINNALhackreturn : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            FindObjectOfType<HackReturnController>().Return(0);
        }
    }

}
