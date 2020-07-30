using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Sniper : Enemy2D
{

    private Vector3 yAxis;

    public Transform barrelU;
    public Transform barrelD;
    public Transform barrelF;
    public Transform barrelDU;
    public Transform barrelDD;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        yAxis = new Vector3(0f, 1f, 0f);
    }

    void Update()
    {
        if (GetTarget() != null)
            Attack();
    }

    public override void Attack()
    {
        LookAtTarget();

        float angle = Vector3.Angle(yAxis, GetTarget().position - transform.position); //getAimingAngle()
        animator.SetFloat("angle", angle);

        if (weapon.CanFire()) {

            if (angle >= 0f && angle < 22.5f)
                mainBarrel = barrelU;
            else if (angle >= 22.5f && angle < 67.5f)
                mainBarrel = barrelDU;
            else if (angle >= 67.5f && angle < 112.5f)
                mainBarrel = barrelF;
            else if (angle >= 112.5f && angle < 157.5f)
                mainBarrel = barrelDD;
            else if (angle >= 157.5f && angle < 180f)
                mainBarrel = barrelD;

            mainBarrel.rotation = Quaternion.Euler(-angle - 90f, (GetTarget().position.x < transform.position.x) ? 90f : -90f, -90f);

            animator.SetTrigger("fire");
            weapon.Fire(mainBarrel);
        }
    }

}
