using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Putin : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Hackable>().SetAction(() =>
        {
            GetComponent<AudioSource>().Play();
        });
    }

}
