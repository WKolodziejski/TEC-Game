﻿using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Player2D player;
    public CinemachineVirtualCamera cam;
    public CinemachineConfiner confiner;
    public GameObject loading;
    public GameObject complete;

    private Lifebar lifebar;
    private Vector3 checkpoint;
    private int scene;
    private int lifes = 3;

    //private Dictionary<Type, int> pontuation;

    void Awake()
    {
        lifebar = FindObjectOfType<Lifebar>();

        StartCoroutine(ILoadScene(5));
    }

    private IEnumerator ILoadScene(int s)
    {
        scene = s;
        checkpoint = new Vector3(0, 5, 0);

        lifebar.gameObject.SetActive(false);
        loading.SetActive(true);

        AsyncOperation load = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!load.isDone)
        {
            yield return null;
        }

        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Grid").GetComponent<PolygonCollider2D>();

        /*pontuation = new Dictionary<Type, int>();

        foreach (Enemy2D e in FindObjectsOfType<Enemy2D>())
        {
            pontuation[e.GetType()] = 0;

            //TODO: não contar se inimigo morrer sozinho

            e.SetOnDieListener(() => pontuation[e.GetType()]++);
        }*/

        yield return new WaitForSeconds(1f);

        loading.SetActive(false);
        lifebar.gameObject.SetActive(true);

        SetupPlayer();
    }

    private void SetupPlayer()
    {
        Player2D p = FindObjectOfType<Player2D>();

        if (p == null)
            p = Instantiate(player, checkpoint, Quaternion.identity, null);
        else
        {
            p.transform.position = checkpoint;
            p.EnableControls();
        }
            
        lifebar.SetPlayer(p);

        cam.Follow = p.transform;
        cam.LookAt = p.transform;

        p.SetOnDieListener(() =>
        {
            cam.Follow = null;
            cam.LookAt = null;

            lifes--;

            if (lifes == 0)
            {
                StartCoroutine(IGameOver());
            } 
            else
            {
                lifebar.SetExtraLifes(lifes);

                StartCoroutine(INewPlayer());
            }
        });
    }

    private IEnumerator INewPlayer()
    {
        yield return new WaitForSeconds(1f);

        SetupPlayer();
    }

    private IEnumerator IGameOver()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameOver");
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpoint = position;
    }

    public void CompleteLevel()
    {
        StartCoroutine(ICompleteLevel());
    }

    private IEnumerator ICompleteLevel()
    {
        lifebar.gameObject.SetActive(false);

        Player2D p = FindObjectOfType<Player2D>();
        p.DisableControlsAndRun();

        yield return new WaitForSeconds(0.5f);

        complete.SetActive(true);

        yield return new WaitForSeconds(2f);

        AsyncOperation load = SceneManager.UnloadSceneAsync(scene);

        while (!load.isDone)
        {
            yield return null;
        }

        complete.SetActive(false);

        StartCoroutine(ILoadScene(scene + 1));
    }

}
