using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Old : MonoBehaviour
{

    public Transform barrelLeft;
    public Transform barrelRight;
    public GameObject projectileCommon;
    public GameObject projectileFollow;
    public GameObject projectileHack;
    public float speed;

    private Transform target;
    private Rigidbody2D rb;
    private int side;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Fire(projectileCommon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Fire(projectileHack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Fire(projectileFollow);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Burst(projectileFollow, 5, 0.6f);
        }
    }

    void FixedUpdate()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Vector3 deltaPosition = speed * dir * Time.deltaTime;
        rb.MovePosition(transform.position + deltaPosition);
    }

    void Fire(GameObject p)
    {
        Transform b = side == 0 ? barrelLeft : barrelRight;

        Instantiate(p, b.position, b.rotation);

        side = (side + 1) % 2;
    }

    void Burst(GameObject p, int amount, float delay)
    {
        StartCoroutine(IBurst(p, amount, delay));
    }

    private IEnumerator IBurst(GameObject p, int amount, float delay)
    {
        for (int i = 0; i < amount; i++)
        {
            Fire(p);

            yield return new WaitForSeconds(delay);
        }
    }

}
