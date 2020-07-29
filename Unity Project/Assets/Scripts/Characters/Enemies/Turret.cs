using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Turret : Enemy2D //criar classe de tiro "complexo"
{

    private Vector3 yAxis;

    public Transform barrelUp;
    public Transform barrelDU;
    public Transform barrelF;
    public Transform barrelDD;
    public Transform barrelDown;

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
        transform.rotation = (transform.position.x > GetTarget().position.x) ? Quaternion.identity : Quaternion.Euler(0f, 180f, 0f); // setShootingDir()   

        float angle = Vector3.Angle(yAxis, GetTarget().position - transform.position); //getAimingAngle()
        animator.SetFloat("angle", angle);

        if (weapon.CanFire()) {
            print(angle);
            
            if (angle > 0f && angle < 22.5f) //setBarrel
            {
                mainBarrel = barrelUp;               
            }
            else
            {
                if (angle > 22.5f && angle < 67.5f)
                {
                    mainBarrel = barrelDU;              
                }
                else
                {
                    if (angle > 67.5f && angle < 112.5f) {
                        mainBarrel = barrelF;
                    } else
                    {
                        if (angle > 112.5f && angle < 157.5f)
                        {
                            mainBarrel = barrelDD;
                        } else
                        {
                            mainBarrel = barrelDown;
                        }
                    }
                }
            }
            weapon.Fire(mainBarrel);
        }
    }

}
