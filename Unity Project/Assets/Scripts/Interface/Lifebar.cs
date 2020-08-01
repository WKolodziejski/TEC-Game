using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{

    public GameObject lf1, lf2, lf3;
    public Image bar;

    private bool animating;
    private float hp;

    public void SetExtraLifes(int l)
    {
        lf1.SetActive(l >= 1);
        lf2.SetActive(l >= 2);
        lf3.SetActive(l >= 3);
    }

    public void SetPlayer(Player2D c)
    {
        hp = c.hp;

        c.SetOnDamageListener(() =>
        {
            if (animating)
                StopAllCoroutines();

            StartCoroutine(IAnim(c.hp / hp));
        });

        if (animating)
            StopAllCoroutines();

        StartCoroutine(IAnim(c.hp / 3));
    }

    private IEnumerator IAnim(float hp)
    {
        animating = true;

        if (bar.fillAmount < hp)
        {
            while (bar.fillAmount < hp)
            {
                bar.fillAmount += 0.05f;

                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (bar.fillAmount > hp)
            {
                bar.fillAmount -= 0.05f;

                yield return new WaitForSeconds(0.01f);
            }
        }

        animating = false;
    }

}
