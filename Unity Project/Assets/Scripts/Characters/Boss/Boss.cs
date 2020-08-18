using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy2D
{

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;

    private bool isAnimating;

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
    
    void FixedUpdate()
    {
        if (!isAnimating)
            rb.position += Vector2.right * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
            movementSpeed *= -1;
    }

    private IEnumerator ICollider()
    {
        isAnimating = true;

        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;

        yield return new WaitForSeconds(15f);

        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = true;

        yield return new WaitForSeconds(1f);

        isAnimating = false;
    }

    protected override void OnDie()
    {
        base.OnDie();

        StartCoroutine(IDie());
    }

    private IEnumerator IDie()
    {
        rb.AddForce(Vector2.right * movementSpeed * 0.5f, ForceMode2D.Impulse);
        rb.gravityScale = 0.05f;
        transform.Rotate(Vector3.forward, -movementSpeed);

        explosion1.SetActive(true);

        yield return new WaitForSeconds(1.25f);

        explosion2.SetActive(true);

        yield return new WaitForSeconds(1.25f);

        explosion3.SetActive(true);

        rb.gravityScale = 1f;
        rb.constraints -= RigidbodyConstraints2D.FreezeRotation;
    }

}