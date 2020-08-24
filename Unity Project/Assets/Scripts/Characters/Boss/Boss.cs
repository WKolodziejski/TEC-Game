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
    public GameObject laser;
    public GameObject shield;

    public float laserDamage = 2f;

    private LineRenderer laserLine;
    private AudioSource laserAudio;
    private bool isAnimating;
    private bool isAttacking;
    private int magnitude;

    protected override void InitializeComponents()
    {
        laserLine = laser.GetComponent<LineRenderer>();
        laserAudio = laser.GetComponent<AudioSource>();

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

        if (laser.activeSelf)
        {
            Vector3 start = mainBarrel.position;
            Vector3 end = new Vector3(start.x, start.y - 10, start.z);

            laserLine.SetPosition(0, start);
            laserLine.SetPosition(1, end);

            laserAudio.pitch = Time.timeScale;
        }
    }

    void FixedUpdate()
    {
        if (!isAnimating && isAttacking)
            rb.position += Vector2.right * movementSpeed * magnitude * Time.fixedDeltaTime;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
            movementSpeed *= -1;
    }   */

    protected override void OnDie()
    {
        base.OnDie();

        StartCoroutine(IDie());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character c = collision.GetComponent<Character>();

            if (c != null)
            {
                if (!c.IsDead())
                {
                    c.TakeDamage(laserDamage, false);
                }
            }
        }

        if (collision.CompareTag("Wall"))
            magnitude *= -1;
    }

    private IEnumerator IDie()
    {
        fire.SetActive(true);
        sparkle.SetActive(false);
        laser.SetActive(false);
        shield.SetActive(false);

        rb.AddForce(Vector2.right * movementSpeed * 0.5f * magnitude, ForceMode2D.Impulse);
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

        shield.SetActive(true);

        yield return new WaitForSeconds(1f);

        isAnimating = false;
    }

    private IEnumerator IAttack()
    {
        magnitude = GetTargetMagnitude();

        animator.SetBool("attack", true);

        yield return new WaitForSeconds(1.5f);

        isAttacking = true;

        shield.SetActive(false);
        laser.SetActive(true);

        yield return new WaitForSeconds(9f);

        animator.SetBool("attack", false);

        laser.SetActive(false);
        shield.SetActive(true);

        isAttacking = false;
    }

}