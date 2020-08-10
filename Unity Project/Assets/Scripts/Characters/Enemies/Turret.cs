
using UnityEngine;

public class Turret : Enemy2D
{

    public Transform barrel1;
    public Transform barrel2;

    void Update()
    {
        if (GetTarget() != null)
            Attack();
    }

    void FixedUpdate()
    {
        if (GetTarget() != null)
        {
            Vector2 lookDir = GetTarget().position - transform.position;
            rb.rotation = -Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg + 90f;
        }  
    }

    public override void Attack()
    {
        weapon.Fire(barrel1);
    }

}
