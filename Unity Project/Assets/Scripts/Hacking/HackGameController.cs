using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Character;
using static HackSceneReference;

public class HackGameController : MonoBehaviour
{

    public GameObject lEASY;
    public GameObject lNORMAL;
    public GameObject lHARD;

    private List<HackingBoss> bosses;
    private List<HackingEnemy> enemies;
    private HackingPlayer player;
    private int actualEnemy;
   
    void Start()
    {
        //DEBUG:
        HackSceneReference.Instance.difficulty = EDifficulty.NORMAL;

        EDifficulty difficulty = HackSceneReference.Instance.GetDifficulty();

        switch (difficulty)
        {
            case EDifficulty.EASY:
                lEASY.SetActive(true);
                break;

            case EDifficulty.NORMAL:
                lNORMAL.SetActive(true);
                break;

            case EDifficulty.HARD:
                lHARD.SetActive(true);
                break;
        }

        bosses = FindObjectsOfType<HackingBoss>().ToList();
        enemies = FindObjectsOfType<HackingEnemy>().ToList();
        player = FindObjectOfType<HackingPlayer>();

        foreach (HackingEnemy e in enemies)
        {
            e.SetOnDieListener(() =>
            {
                enemies.Remove(e);

                if (enemies.Count == 0)
                {
                    foreach (HackingBoss b in bosses)
                    {
                        b.DisableShield();
                        b.SetAgressive();
                    }
                }
            });
        }

        foreach (HackingBoss b in bosses)
        {
            b.SetOnDieListener(() =>
            {
                bosses.Remove(b);

                if (bosses.Count == 0)
                {
                    StartCoroutine(IPlayExit(true));
                }
            });
        }

        player.SetOnDieListener(() =>
        {
            StartCoroutine(IPlayExit(false));
        });

        StartCoroutine(IPlayEnter());
    }

    void Update()
    {
        if (enemies.Count > 0)
        {
            actualEnemy = (actualEnemy + 2) % enemies.Count;
            enemies[actualEnemy].Fire();
        }

        if (Input.GetKey(KeyCode.H))
            StartCoroutine(IPlayExit(true));
    }

    private IEnumerator IPlayEnter()
    {
        //TODO: cutscene "start hacking"

        //Time.timeScale = 0.1f;
        //Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSeconds(1f * Time.timeScale);

        //TODO: fade
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private IEnumerator IPlayExit(bool won)
    {
        //TODO: fade
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        //TODO: cutscene "finish"

        yield return new WaitForSeconds(1f * Time.timeScale);

        FindObjectOfType<HackReturnController>().Return(won);
    }

}
