using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy2D
{

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject fire;
    public GameObject sparkle;

    private bool isAnimating;

    protected override void InitializeComponents()
    {
        SetEnabled(true);
    }

    public override void Attack()
    {
        StartCoroutine(IAttack());
    }

    void Start()
    {
        StartCoroutine(ICollider());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Attack();
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

    protected override void OnDie()
    {
        base.OnDie();

        StartCoroutine(IDie());
    }

    private IEnumerator IDie()
    {
        fire.SetActive(true);
        sparkle.SetActive(false);

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

    private IEnumerator IAttack()
    {
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(1.5f);

        weapon.Fire(mainBarrel);
    }

}