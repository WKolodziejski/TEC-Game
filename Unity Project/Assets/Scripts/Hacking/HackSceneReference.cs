using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class HackSceneReference : MonoBehaviour
{
    //protected HackSceneReference() { }

    public CinemachineVirtualCamera cam;
    public PlayableDirector transitionEnter;
    public PlayableDirector transitionReturn;

    private GameObject[] objs;
    private bool isHacking;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Enter(Transform target, EDifficulty difficulty)
    {
        Debug.Log("Entering...");

        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        cam.LookAt = target;
        cam.Follow = target;
        transitionEnter.Play();

        StartCoroutine(IEnter(difficulty));
    }

    private IEnumerator IEnter(EDifficulty difficulty)
    {
        yield return new WaitForSeconds(1.5f * Time.timeScale);

        EnterHackGame(difficulty);
    }

    public void Return(bool won)
    {
        transitionReturn.Play();

        StartCoroutine(IReturn(won));
    }

    private IEnumerator IReturn(bool won)
    {
        Debug.Log("Returning...");

        yield return new WaitForSeconds(1f * Time.timeScale);

        ReturnHackGame(won);

        cam.Follow = null;
        cam.LookAt = null;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void EnterHackGame(EDifficulty difficulty)
    {
        if (!isHacking)
        {
            this.isHacking = true;
            this.difficulty = difficulty;
            this.objs = FindObjectsOfType<GameObject>();

            SceneManager.LoadScene("HackScene", LoadSceneMode.Additive);

            foreach (GameObject o in objs)
            {
                DontDestroyOnLoad(o);

                if (o.GetComponent<DontDestroy>() == null)
                {
                    o.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("Already hacking");
        }
    }

    private void ReturnHackGame(bool won)
    {
        if (isHacking)
        {
            this.won = won;
            this.isHacking = false;

            SceneManager.UnloadSceneAsync("HackScene");

            foreach (GameObject o in objs)
            {
                if (o != null)
                    o.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Not hacking");
        }
    }

    public EDifficulty difficulty;

    public EDifficulty GetDifficulty()
    {
        return difficulty;
    }

    public enum EDifficulty
    {
        EASY, NORMAL, HARD
    }

    private bool won;

    public bool Won()
    {
        return won;
    }

}
