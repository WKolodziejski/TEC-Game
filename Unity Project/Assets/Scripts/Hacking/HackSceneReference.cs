using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackSceneReference : Singleton<HackSceneReference>
{
    protected HackSceneReference() { }

    private GameObject[] objs;

    public void EnterHackGame(EDifficulty difficulty)
    {
        this.difficulty = difficulty;
        this.objs = FindObjectsOfType<GameObject>();

        SceneManager.LoadScene(0, LoadSceneMode.Additive);

        foreach (GameObject o in objs)
        {
            DontDestroyOnLoad(o);

            if (o.GetComponent<DontDestroy>() == null)
            {
                o.SetActive(false);
            }
        }
    }

    public void ReturnHackGame(float returnDamage)
    {
        this.returnDamage = returnDamage;

        SceneManager.UnloadSceneAsync(0);

        foreach (GameObject o in objs)
        {
            if (o != null)
                o.SetActive(true);
        }
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

    private float returnDamage;

    public float GetDamage()
    {
        return returnDamage;
    }

}
