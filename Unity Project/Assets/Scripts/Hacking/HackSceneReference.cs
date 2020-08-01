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

    private Action<bool> onReturn;
    private GameObject[] objs;
    private bool isHacking;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Enter(Transform target, EDifficulty difficulty, Action<bool> onReturn)
    {
        this.onReturn = onReturn;

        StartCoroutine(IEnter(target, difficulty));
    }

    private IEnumerator IEnter(Transform target, EDifficulty difficulty)
    {
        Debug.Log("Entering...");

        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        cam.LookAt = target;
        cam.Follow = target;
        transitionEnter.Play();

        yield return new WaitForSeconds(2f * Time.timeScale);

        if (!isHacking)
        {
            isHacking = true;
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

    public void Return(bool won)
    {
        StartCoroutine(IReturn(won));
    }

    private IEnumerator IReturn(bool won)
    {
        Debug.Log("Returning...");

        yield return new WaitForSeconds(0.5f * Time.timeScale);

        transitionReturn.Play();

        if (isHacking)
        {
            isHacking = false;

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

        yield return new WaitForSeconds(0.5f * Time.timeScale);

        cam.Follow = null;
        cam.LookAt = null;

        yield return new WaitForSeconds(2f * Time.timeScale);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        onReturn(won);
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

}
