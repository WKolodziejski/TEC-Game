using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Sniper : Enemy2D
{

    private Vector3 yAxis;

    public Transform barrelF;
    public Transform barrelDU;
    public Transform barrelDD;

    protected override void InitializeComponents()
    {
        SetEnemySpawner();
        attackAction = CanAttack;
        yAxis = new Vector3(0f, 1f, 0f);
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (GetTarget() != null) {
            attackAction(); 
        }
    }

    public override void Attack()
    {
        LookAtTarget();

        float angle = Vector3.Angle(yAxis, GetTarget().position - transform.position); //getAimingAngle()
        animator.SetFloat("angle", angle);

        if (weapon.CanFire()) {

            if (angle > 0f && angle < 70f) //setBarrel
            {
                mainBarrel = barrelDU;
            } else {
                if (angle > 70f && angle < 110f)
                {
                    mainBarrel = barrelF;
                } else
                {
                    mainBarrel = barrelDD;
                }
            }

            mainBarrel.rotation = Quaternion.Euler(-angle - 90f, (GetTarget().position.x < transform.position.x) ? 90f : -90f, -90f);

            weapon.Fire(mainBarrel);
        }
    }

}
