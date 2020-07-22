using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Controller player;
    public CinemachineVirtualCamera cam;
    public CinemachineConfiner confiner;

    private Lifebar lifebar;
    private Vector3 checkpoint;
    private int lifes = 3;

    void Awake()
    {
        StartCoroutine(ILoadScene("TESTE"));

        lifebar = FindObjectOfType<Lifebar>();
    }

    void Start()
    {
        SetupPlayer(Instantiate(player, checkpoint, Quaternion.identity, null));
    }

    private IEnumerator ILoadScene(string scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!load.isDone)
        {
            Debug.Log(load.progress);

            yield return null;
        }

        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Grid").GetComponent<PolygonCollider2D>();
    }

    private void SetupPlayer(Controller p)
    {

        lifebar.SetPlayer(p);

        cam.Follow = p.transform;
        cam.LookAt = p.transform;

        p.SetOnDieListener(() =>
        {
            cam.Follow = null;
            cam.LookAt = null;

            Debug.Log("Morri 1");

            lifes--;

            if (lifes == 0)
            {
                //gameover
            } 
            else
            {
                Debug.Log("Morri 2");

                lifebar.SetExtraLifes(lifes);

                StartCoroutine(INewPlayer());
            }
        });
    }

    public void SetCheckpoint(Vector3 position)
    {
        Debug.Log(position);
        checkpoint = position;
    }

    private IEnumerator INewPlayer()
    {
        yield return new WaitForSeconds(1f);

        SetupPlayer(Instantiate(player, checkpoint, Quaternion.identity, null));
    }

}
