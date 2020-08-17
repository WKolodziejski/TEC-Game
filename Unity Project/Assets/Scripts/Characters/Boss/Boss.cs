using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy2D
{

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;

    protected override void InitializeComponents()
    {
        SetEnabled(true);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        StartCoroutine(ICollider());
    }
    
    void Update()
    {
        
    }

    private IEnumerator ICollider()
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;

        yield return new WaitForSeconds(1f);

        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = true;
    }

    protected override void OnDie()
    {
        base.OnDie();

        StartCoroutine(IDie());
    }

    private IEnumerator IDie()
    {
        explosion1.SetActive(true);

        yield return new WaitForSeconds(1.25f);

        explosion2.SetActive(true);

        yield return new WaitForSeconds(1.25f);

        explosion3.SetActive(true);

        rb.gravityScale = 1f;
        rb.constraints -= RigidbodyConstraints2D.FreezeRotation;
        transform.Rotate(Vector3.forward, 10f);
    }

}