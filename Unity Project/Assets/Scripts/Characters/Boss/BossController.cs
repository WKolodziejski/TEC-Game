﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    public Transform insideLeft;
    public Transform insideRight;
    public Transform outsideLeft;
    public Transform outsideRight;
    public Boss bossPrefab;
    public Hackable hackable;
    public AudioSource audio1;
    public GameObject audio2;
    public CinemachineVirtualCamera cam;
    public Image fadeOut;

    private AudioSource audioSource;
    private Boss boss;
    private bool hacked;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        hackable.SetAction(() =>
        {
            if (!hacked)
            {
                hacked = true;
                Open();
            }
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Open();

        if (Input.GetKeyDown(KeyCode.I))
            StartCoroutine(Utils.FadeOutAudio(audioSource));
    }

    public void Open()
    {
        StartCoroutine(IOpen());
    }

    private IEnumerator IOpen()
    {
        FindObjectOfType<Lifebar>()?.gameObject.SetActive(false);

        GameController.canPause = false;

        yield return new WaitForSeconds(1f);

        audio2.SetActive(true);

        yield return new WaitForSeconds(3f);

        FindObjectOfType<Player2D>().DisableControls();

        boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

        boss.SetOnDieListener(() =>
        {
            StartCoroutine(IFinish());
        });

        cam.m_Priority = 20;
        cam.Follow = boss.transform;
        cam.LookAt = boss.transform;

        while (insideLeft.localRotation.eulerAngles.z < 90f)
        {
            insideLeft.Rotate(Vector3.forward, 0.5f, Space.Self);
            insideRight.Rotate(Vector3.forward, -0.5f);

            if (insideLeft.localRotation.eulerAngles.z > 45f)
            {
                outsideLeft.Rotate(Vector3.forward, 0.5f);
                outsideRight.Rotate(Vector3.forward, -0.5f);
            }

            yield return new WaitForSeconds(0.05f);
        }

        while (boss.transform.position.y >= 2)
        {
            boss.transform.position -= Vector3.up * 0.05f;
            yield return new WaitForSeconds(0.025f);
        }

        cam.m_Priority = 0;

        FindObjectOfType<Lifebar>()?.gameObject.SetActive(true);

        FindObjectOfType<Player2D>().EnableControls();

        GameController.canPause = true;
    }

    private IEnumerator IFinish()
    {
        GameController.canPause = false;

        FindObjectOfType<Lifebar>()?.gameObject.SetActive(false);

        yield return new WaitForSeconds(7f);

        StartCoroutine(Utils.FadeInImg(fadeOut, 1f));
        StartCoroutine(Utils.FadeOutAudio(audio1));

        FindObjectOfType<Player2D>().DisableControls();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Credits");
    }

}
