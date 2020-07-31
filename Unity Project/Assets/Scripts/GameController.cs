using Cinemachine;
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

    private Lifebar lifebar;
    private Vector3 checkpoint;
    private int lifes = 3;

    private Dictionary<Type, int> pontuation;

    void Awake()
    {
        StartCoroutine(ILoadScene("TESTE"));

        lifebar = FindObjectOfType<Lifebar>();
    }

    private IEnumerator ILoadScene(string scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!load.isDone)
        {
            yield return null;
        }

        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Grid").GetComponent<PolygonCollider2D>();

        pontuation = new Dictionary<Type, int>();

        foreach (Enemy2D e in FindObjectsOfType<Enemy2D>())
        {
            pontuation[e.GetType()] = 0;

            //TODO: não contar se inimigo morrer sozinho

            e.SetOnDieListener(() => pontuation[e.GetType()]++);
        }

        SetupPlayer(Instantiate(player, checkpoint, Quaternion.identity, null));
    }

    private void SetupPlayer(Player2D p)
    {

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

    public void SetCheckpoint(Vector3 position)
    {
        checkpoint = position;
    }

    private IEnumerator INewPlayer()
    {
        yield return new WaitForSeconds(1f);

        SetupPlayer(Instantiate(player, checkpoint, Quaternion.identity, null));
    }

    private IEnumerator IGameOver()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameOver");
    }

}
