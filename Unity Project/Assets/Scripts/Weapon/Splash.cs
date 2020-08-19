using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{

    public float range = 2f;
    public float damage = 3f;
    public float duration = 0.1f;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            Character c = collision.GetComponent<Character>();

            if (c != null)
                if (!c.IsDead())
                    c.TakeDamage(damage, false);
        }
    }

    private IEnumerator IDestroy()
    {
        yield return new WaitForSeconds(duration);

        enabled = false;
    }

}
