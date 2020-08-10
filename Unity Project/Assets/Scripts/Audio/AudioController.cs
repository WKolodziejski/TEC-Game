using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource hack;

    private AudioSource actual;

    public void EnterHack()
    {
        foreach (AudioTrigger a in FindObjectsOfType<AudioTrigger>())
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
        float startVolume = a.volume;

        while (a.volume > 0)
        {
            a.volume -= startVolume * Time.deltaTime / 2f;

            yield return null;
        }

        a.mute = true;
    }

    IEnumerator FadeIn(AudioSource a)
    {
        a.mute = false;

        float startVolume = 0.2f;

        a.volume = 0;

        while (a.volume < 1.0f)
        {
            a.volume += startVolume * Time.deltaTime;

            yield return null;
        }
    }

}
