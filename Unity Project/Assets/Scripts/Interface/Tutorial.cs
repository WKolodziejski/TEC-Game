using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public Text text;
    public Image image;
    public Hackable terminal;
    public GameObject sparkle;

    void Start()
    {
        StartCoroutine(IFirst());

        terminal.SetAction(() => SetText("You can hack some objects and enemies to help through your journey"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetText("Hold H to start hacking");
            sparkle.SetActive(false);
        }
    }

    IEnumerator IFirst()
    {
        yield return new WaitForSeconds(2f);

        SetText("Walk to the terminal");
    }

    private void SetText(string txt)
    {
        StopAllCoroutines();

        StartCoroutine(IText(txt));
    }

    IEnumerator IText(string txt)
    {
        text.text = txt;

        FadeIn();

        yield return new WaitForSeconds(3f);

        FadeOut();
    }

    private void FadeIn()
    {
        StartCoroutine(FadeInText());
        StartCoroutine(FadeInImg());
    }

    private void FadeOut()
    {
        StartCoroutine(FadeOutText());
        StartCoroutine(FadeOutImg());
    }

    IEnumerator FadeOutText()
    {
        Color tc = text.color;
        tc.a = 0.5f;

        while (tc.a > 0f)
        {
            tc.a -= 0.1f;
            text.color = tc;

            yield return new WaitForSeconds(0.05f);
        }

        text.text = "";
    }

    IEnumerator FadeInText()
    {
        Color tc = text.color;
        tc.a = 0;

        while (tc.a < 1f)
        {
            tc.a += 0.1f;
            text.color = tc;

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOutImg()
    {
        Color ic = image.color;
        ic.a = 1f;

        while (ic.a > 0f)
        {
            ic.a -= 0.1f;
            image.color = ic;

            yield return new WaitForSeconds(0.1f);
        }

        ic.a = 0f;
        image.color = ic;
    }

    IEnumerator FadeInImg()
    {
        Color ic = image.color;
        ic.a = 0;

        while (ic.a < 0.5f)
        {
            ic.a += 0.1f;
            image.color = ic;

            yield return new WaitForSeconds(0.1f);
        }
    }

}
