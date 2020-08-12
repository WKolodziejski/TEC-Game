using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Sniper : ComplexShooter //TODO: consertar de forma decente os sniperes atirando juntos e remover função da weapon
{
    public readonly float lockingTime = 0.5f;
    private bool locked;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();
        locked = false; 
        weapon.RandomizeFireRate(); //fix porco pros sniperes atirando todos juntos
    }

    void Update()
    {
        if (GetTarget() != null)
            Attack();
        else
            animator.SetFloat("angle", -1);
    }

    public override void Attack()
    {
        if (!locked)
        {
            Aim();
            mainBarrel.rotation = Quaternion.Euler(-aimingAngle - 90f, (GetTarget().position.x < transform.position.x) ? 90f : -90f, -90f);

            if (weapon.CanFire())
                StartCoroutine(Locking());
        }
    }

    private IEnumerator Locking()
    {
        locked = true;
        yield return new WaitForSeconds(lockingTime);
        animator.SetTrigger("fire");
        weapon.Fire(mainBarrel);
        locked = false;
    }

}
