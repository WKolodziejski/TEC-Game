using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private AudioSource hack;
    private List<AudioSource> audios;
    private int actual;

    void Start()
    {
        hack = GetComponent<AudioSource>();
        audios = new List<AudioSource>();

        foreach (AudioTrigger a in GetComponentsInChildren<AudioTrigger>())
            audios.Add(a.GetAudioSource());
    }

    public void SetActive(AudioSource audioSource)
    {
        StartCoroutine(FadeOut(audios[actual]));

        actual = audios.IndexOf(audioSource);

        StartCoroutine(FadeIn(audios[actual]));
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
