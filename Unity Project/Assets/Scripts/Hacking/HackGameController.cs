using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HackingCharacter;

public class HackGameController : MonoBehaviour
{

    private HackingBoss[] bosses;
    private HackingEnemy[] enemies;
    private HackingPlayer player;
   
    void Start()
    {
        bosses = FindObjectsOfType<HackingBoss>();
        enemies = FindObjectsOfType<HackingEnemy>();
        player = FindObjectOfType<HackingPlayer>();

        foreach (HackingEnemy e in enemies)
        {
            e.SetOnDieListener(() =>
            {
                if (enemies.Length == 0)
                {
                    foreach (HackingBoss b in bosses)
                    {
                        //b.RemoveShield();
                        //b.SetAgressive();
                    }
                }
            });
        }

        foreach (HackingBoss b in bosses)
        {
            b.SetOnDieListener(() =>
            {
                if (bosses.Length == 0)
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
