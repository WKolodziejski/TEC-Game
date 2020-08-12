using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class HackSceneReference : MonoBehaviour
{

    public CinemachineVirtualCamera cam;
    public PlayableDirector transitionEnter;
    public PlayableDirector transitionReturn;

    private Action<bool> onReturn;
    private GameObject[] objs;
    private bool isHacking;

    public void Enter(Transform target, EDifficulty difficulty, Action<bool> onReturn)
    {
        this.onReturn = onReturn;

        StartCoroutine(IEnter(target, difficulty));
    }

    private IEnumerator IEnter(Transform target, EDifficulty difficulty)
    {
        Time.timeScale = 0.01f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        cam.LookAt = target;
        cam.Follow = target;
        transitionEnter.Play();

        FindObjectOfType<AudioController>().EnterHack();

        //while (!transitionEnter.)

        yield return new WaitForSeconds(1.9f * Time.timeScale);

        if (!isHacking)
        {
            isHacking = true;
            this.difficulty = difficulty;
            this.objs = FindObjectsOfType<GameObject>();

            AsyncOperation s = SceneManager.LoadSceneAsync("HackScene", LoadSceneMode.Additive);
            //s.allowSceneActivation = false;

           while (!s.isDone)
                yield return null;

            foreach (GameObject o in objs)
                if (o.GetComponent<DontDestroy>() == null)
                    o.SetActive(false);
        }
    }

    public void Return(bool won)
    {
        StartCoroutine(IReturn(won));
    }

    private IEnumerator IReturn(bool won)
    {
        yield return new WaitForSeconds(0.5f * Time.timeScale);

        transitionReturn.Play();

        FindObjectOfType<AudioController>().ReturnHack();

        if (isHacking)
        {
            isHacking = false;

            SceneManager.UnloadSceneAsync("HackScene");

            foreach (GameObject o in objs)
                if (o != null)
                    o.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f * Time.timeScale);

        cam.Follow = null;
        cam.LookAt = null;

        yield return new WaitForSeconds(2f * Time.timeScale);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        onReturn(won);
    }

    private EDifficulty difficulty;

    public EDifficulty GetDifficulty()
    {
        return difficulty;
    }

    public enum EDifficulty
    {
        EASY, NORMAL, HARD
    }

}
