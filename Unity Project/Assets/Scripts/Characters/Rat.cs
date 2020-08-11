using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemy2D
{

    protected override void InitializeComponents()
    {
        Destroy(gameObject, 20f);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.right * movementSpeed * Time.deltaTime * -1f;
    }

    protected override void OnDie()
    {
        base.OnDie();

        Destroy(gameObject);
    }

    public override void Attack()
    {
        
    }

}
