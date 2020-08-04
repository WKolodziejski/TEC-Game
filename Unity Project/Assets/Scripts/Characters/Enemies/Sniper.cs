using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Sniper : ComplexShooter
{
    void Update()
    {
        if (GetTarget() != null)
            Attack();
        else
            animator.SetFloat("angle", -1);
    }

    public override void Attack()
    {
        Aim();
        mainBarrel.rotation = Quaternion.Euler(-aimingAngle - 90f, (GetTarget().position.x < transform.position.x) ? 90f : -90f, -90f);

        if(weapon.CanFire())
            animator.SetTrigger("fire");

        weapon.Fire(mainBarrel);
    }

}
