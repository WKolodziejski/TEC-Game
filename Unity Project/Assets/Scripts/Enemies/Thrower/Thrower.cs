using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    public ThrowerMissile missile;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            animator.SetTrigger("Throw");
            missile.Fire();
        }
    }

}
