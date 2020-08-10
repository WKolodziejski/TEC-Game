using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HackSceneReference;

[RequireComponent(typeof(Collider2D))]
public class Hackable : MonoBehaviour
{

    public EDifficulty difficulty;

    private bool isHacked;
    private GameObject portal;
    protected Action action;

    public void Hack(GameObject p)
    {
        if (!isHacked)
        {
            isHacked = true;

            portal = Instantiate(p, transform.position, Quaternion.identity, transform);

            FindObjectOfType<HackSceneReference>().Enter(transform, difficulty, (won) =>
            {
                Destroy(portal);

                if (won)
                {
                    if (gameObject.GetComponent<Character>() != null)
                        gameObject.GetComponent<Character>().Kill();
                    else
                        action?.Invoke();
                }
                else
                {
                    FindObjectOfType<Player2D>().TakeDamage(5f, true);
                }
            });
        }
        else
        {
            action?.Invoke();
        }
    }

    public void SetAction(Action action)
    {
        this.action = action;
    }

}
