using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private AudioSource hack;
    private AudioSource actual;

    void Start()
    {
        hack = GetComponent<AudioSource>();
    }

    public void EnterHack()
    {
        foreach (AudioTrigger a in GetComponentsInChildren<AudioTrigger>())
        {
            AudioSource source = a.GetAudioSource();

            if (!source.mute)
            {
                actual = source;
                break;
            }
        }

        StartCoroutine(FadeIn(hack));
        StartCoroutine(FadeOut(actual));
    }

    public void ReturnHack()
    {
        StartCoroutine(FadeIn(actual));
        StartCoroutine(FadeOut(hack));

        actual = null;
    }

    IEnumerator FadeOut(AudioSource a)
    {
        a.volume = 1;

        while (a.volume > 0)
        {
            a.volume -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        a.mute = true;
    }

    IEnumerator FadeIn(AudioSource a)
    {
        a.mute = false;
        a.volume = 0;

        while (a.volume < 1.0f)
        {
            a.volume += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

}
