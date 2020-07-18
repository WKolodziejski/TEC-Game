using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
            collision.GetComponent<Character>().InstaKill();
    }

}
