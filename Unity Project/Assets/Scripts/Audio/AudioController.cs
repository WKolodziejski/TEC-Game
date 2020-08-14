using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private AudioSource hack;
    private List<AudioSource> audios;
    private int actual;
    private bool firstSong = true;

    void Start()
    {
        hack = GetComponent<AudioSource>();
    }

    public void SetActive(AudioSource audioSource)
    {
        if (audios == null)
        {
            audios = new List<AudioSource>();

            foreach (AudioTrigger a in GetComponentsInChildren<AudioTrigger>())
                audios.Add(a.GetAudioSource());
        }

        if (firstSong)
        {
            firstSong = false;

            actual = audios.IndexOf(audioSource);

            StartCoroutine(FadeIn(audios[actual]));
        }
        else
        {
            StartCoroutine(FadeOut(audios[actual]));

            actual = audios.IndexOf(audioSource);

            StartCoroutine(FadeIn(audios[actual]));
        }
    }

    public void EnterHack()
    {
        StartCoroutine(FadeIn(hack));
        StartCoroutine(FadeOut(audios[actual]));
    }

    public void ReturnHack()
    {
        StartCoroutine(FadeIn(audios[actual]));
        StartCoroutine(FadeOut(hack));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(audios[actual]));
    }

    IEnumerator FadeOut(AudioSource a)
    {
        a.volume = 1;

        while (a.volume > 0)
        {
            a.volume -= 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
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

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

}
