using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Turret : ComplexShooter
{
    void Update()
    {
        if (GetTarget() != null)
            Attack();
    }

    public override void Attack()
    {
        Aim();
        weapon.Fire(mainBarrel);
    }

}
