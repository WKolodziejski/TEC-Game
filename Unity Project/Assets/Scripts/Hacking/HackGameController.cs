using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using static Character;
using static HackSceneReference;

public class HackGameController : MonoBehaviour
{

    public GameObject lEASY;
    public GameObject lNORMAL;
    public GameObject lHARD;

    public GameObject start;
    public GameObject sucess;
    public GameObject failed;

    private List<BossHack> bosses;
    private List<EnemyHack> enemies;
    private PlayerHack player;
    private float enemyCooldown;
   
    void Start()
    {
        //EDifficulty difficulty = FindObjectOfType<HackSceneReference>().GetDifficulty();

        switch (EDifficulty.HARD)
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

        bosses = FindObjectsOfType<BossHack>().ToList();
        enemies = FindObjectsOfType<EnemyHack>().ToList();
        player = FindObjectOfType<PlayerHack>();

        bosses.ForEach((BossHack b) => enemies.Remove(b));

        foreach (EnemyHack e in enemies)
        {
            e.SetOnDieListener(() =>
            {
                enemies.Remove(e);

                if (enemies.Count == 0)
                {
                    foreach (BossHack b in bosses)
                    {
                        b.DisableShield();
                        b.SetAgressive();
                    }
                }
            });
        }

        foreach (BossHack b in bosses)
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

        player.SetOnDieListener(() => StartCoroutine(IPlayExit(false)));

        StartCoroutine(IPlayEnter());
    }

    void Update()
    {
        if (enemies.Count > 0 && Time.time - enemyCooldown >= 0.5f)
        {
            enemyCooldown = Time.time;
            enemies[UnityEngine.Random.Range(0, enemies.Count)].Fire();
        }
            
        if (Input.GetKey(KeyCode.H))
            StartCoroutine(IPlayExit(true));
    }

    private IEnumerator IPlayEnter()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        start.SetActive(true);

        yield return new WaitForSeconds(1f * Time.timeScale);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        start.SetActive(false);
    }

    private IEnumerator IPlayExit(bool won)
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (won)
            sucess.SetActive(true);
        else
            failed.SetActive(true);

        yield return new WaitForSeconds(1f * Time.timeScale);

        FindObjectOfType<HackSceneReference>().Return(won);
    }

}
