using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackSceneReference : Singleton<HackSceneReference>
{
    protected HackSceneReference() { }

    private GameObject[] objs;
    private bool isHacking;
    public void EnterHackGame(EDifficulty difficulty)
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

    public void ReturnHackGame(float returnDamage)
    {
        if (isHacking)
        {
            SceneManager.UnloadSceneAsync("HackScene");

            foreach (GameObject o in objs)
            {
                if (o != null)
                    o.SetActive(true);
            }

            this.returnDamage = returnDamage;
            this.isHacking = false;
        }
        else
        {
            Debug.Log("Not hacking");
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
