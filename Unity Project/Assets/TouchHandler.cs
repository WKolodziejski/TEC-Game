using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool jump;
    public bool hack;
    public bool fire;

    public void jumpBtUp(){
        jump = false;
    }
    
    public void jumpBtDown(){
        jump = true;
    }

    public void fireBtUp(){
        fire = false;
    }
    
    public void fireBtDown(){
        fire = true;
    }

    public void hackBtUp(){
        hack = false;
    }
    
    public void hackBtDown(){
        hack = true;
    }

}
