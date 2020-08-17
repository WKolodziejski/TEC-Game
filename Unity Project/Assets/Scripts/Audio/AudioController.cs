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

            StartCoroutine(Utils.FadeIn(audios[actual]));
        }
        else
        {
            StartCoroutine(Utils.FadeOut(audios[actual]));

            actual = audios.IndexOf(audioSource);

            StartCoroutine(Utils.FadeIn(audios[actual]));
        }
    }

    public void EnterHack()
    {
        StartCoroutine(Utils.FadeIn(hack));
        StartCoroutine(Utils.FadeOut(audios[actual]));
    }

    public void ReturnHack()
    {
        StartCoroutine(Utils.FadeIn(audios[actual]));
        StartCoroutine(Utils.FadeOut(hack));
    }

    public void FadeOut()
    {
        StartCoroutine(Utils.FadeOut(audios[actual]));
    }

}
