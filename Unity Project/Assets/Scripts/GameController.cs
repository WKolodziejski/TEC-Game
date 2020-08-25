using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Player2D player;
    public CinemachineVirtualCamera cam;
    public CinemachineConfiner confiner;
    public GameObject loading;
    public GameObject complete;
    public Volume damage;
    public static bool isPaused;
    public static bool canPause;

    private GameMenuButtons menu;
    private Lifebar lifebar;
    private Vector3 checkpoint;
    private CameraRange range;
    private int scene;
    private int lifes = 4;

    //private Dictionary<Type, int> pontuation;

    void Awake()
    {
        lifebar = FindObjectOfType<Lifebar>();
        range = FindObjectOfType<CameraRange>();
        menu = FindObjectOfType<GameMenuButtons>();
        menu.gameObject.SetActive(false);

        StartCoroutine(ILoadScene(4));
    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            if (canPause)
            {
                if (isPaused)
                {
                    menu.Resume();
                }
                else
                {
                    menu.gameObject.SetActive(true);
                    menu.Pause();
                }
            }
        }
    }

    private IEnumerator ILoadScene(int s)
    {
        scene = s;

        checkpoint = scene == 7 ? new Vector3(0, 2.5f, 0) : new Vector3(-1, 5, 0);

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
        range.SetEnabled(true);

        SetupPlayer();

        canPause = true;
    }

    private void SetupPlayer()
    {
        Player2D p = FindObjectOfType<Player2D>();

        if (p == null)
        {
            p = Instantiate(player, checkpoint, Quaternion.identity, null);

            p.SetOnDamageListener(() => damage.weight = (float)(Math.Exp(p.maxHP - p.GetHP()) / 100));

            p.SetOnDieListener(() =>
            {
                lifes--;

                if (lifes == 0)
                {
                    StartCoroutine(IGameOver());
                }
                else
                {
                    lifebar.SetExtraLifes(lifes);

                    StartCoroutine(INewPlayer());

                    StartCoroutine(IAnim());
                }
            });

            lifebar.SetPlayer(p);
        }
        else
        {
            p.transform.position = checkpoint;
            p.EnableControls();
        }

        if (scene == 7)
        {
            cam.Follow = null;
            cam.LookAt = null;
            cam.enabled = false;
            Camera.main.transform.position = new Vector3(0, 0, -10);
            cam.transform.position = new Vector3(0, 0, -10);
            cam.enabled = true;
        }
        else
        {
            cam.Follow = p.transform;
            cam.LookAt = p.transform;
        }
    }

    private IEnumerator INewPlayer()
    {
        yield return new WaitForSeconds(2.5f);

        SetupPlayer();
    }

    private IEnumerator IGameOver()
    {
        canPause = false;

        yield return new WaitForSeconds(2.5f);

        foreach (GameObject o in FindObjectsOfType<GameObject>())
            Destroy(o);

        SceneManager.LoadScene("GameOver");
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpoint = position;
    }

    public void CompleteLevel(CinemachineVirtualCamera vcam)
    {
        StartCoroutine(ICompleteLevel(vcam));
    }

    private IEnumerator ICompleteLevel(CinemachineVirtualCamera vcam)
    {
        if (vcam != null)
            vcam.m_Priority = 100;

        canPause = false;

        range.SetEnabled(false);
        lifebar.gameObject.SetActive(false);

        Player2D p = FindObjectOfType<Player2D>();
        p.DisableControls();
        p.Walk(1f);

        foreach (Enemy2D e in FindObjectsOfType<Enemy2D>())
            e.SetEnabled(false);

        foreach (Bullet b in FindObjectsOfType<Bullet>())
            Destroy(b.gameObject);

        yield return new WaitForSeconds(0.5f);

        FindObjectOfType<AudioController>()?.FadeOut();

        complete.SetActive(true);

        yield return new WaitForSeconds(3f);

        if (vcam != null)
            vcam.m_Priority = 0;

        AsyncOperation load = SceneManager.UnloadSceneAsync(scene);

        while (load != null)
            yield return null;

        complete.SetActive(false);

        StartCoroutine(ILoadScene(scene + 1));
    }

    private IEnumerator IAnim()
    {
        yield return new WaitForSeconds(2.5f);

        while (damage.weight > 0)
        {
            damage.weight -= 0.05f;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void AddExtraLifes(int l)
    {
        int tmp = lifes + l;

        lifes = tmp <= 4 ? tmp : 4;

        lifebar.SetExtraLifes(lifes);
    }

}
