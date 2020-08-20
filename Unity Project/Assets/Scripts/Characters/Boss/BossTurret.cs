using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : Turret
{

    private const int MAX_TURN = 60;
    private float startAngle;
    private bool shoot;

    protected override void InitializeComponents()
    {
        SetEnabled(true);
    }

    void Start()
    {
        startAngle = transform.eulerAngles.z;
    }
        
    private float GetAngle()
    {
        if (GetTarget() != null)
        {
            Vector2 direction = GetTarget().transform.position - mainBarrel.position;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else
        {
            return startAngle;
        }
    }

    public override void Attack()
    {
        if (shoot)
            base.Attack();
    }

    protected override void Aim()
    {
        float min = startAngle - MAX_TURN;
        float max = startAngle + MAX_TURN;
        
        (min, max) = ClampAngle(min, max);

        Quaternion rotation = Quaternion.AngleAxis(GetAngle(), Vector3.forward);

        shoot = true;

        if (startAngle - MAX_TURN < 0) 
        {
            if (rotation.eulerAngles.z > max && rotation.eulerAngles.z < min)
            {
                rotation = Quaternion.AngleAxis(startAngle, Vector3.forward);
                shoot = false;
            }
        } 
        else if (rotation.eulerAngles.z > max || rotation.eulerAngles.z < min)
        {
            rotation = Quaternion.AngleAxis(startAngle, Vector3.forward);
            shoot = false;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }

    private (float, float) ClampAngle(float min, float max)
    {
        if (min < 0) min += 360;
        if (max > 360) max += 360;

        return (min, max);
    }

    public void IncreaseFireRate(float fr)
    {
        weapon.fireRate -= fr;
    }

}
