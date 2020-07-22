using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Player2D player;
    public CinemachineVirtualCamera cam;

    private Lifebar lifebar;
    private Vector3 checkpoint;
    private int lifes = 3;

    void Awake()
    {
        SceneManager.LoadSceneAsync("TESTE", LoadSceneMode.Additive);
        lifebar = FindObjectOfType<Lifebar>();
    }

    void Start()
    {
        SetupPlayer(Instantiate(player, checkpoint, Quaternion.identity, null));
    }

    private void SetupPlayer(Player2D p)
    {

        lifebar.SetPlayer(p);

        cam.Follow = p.transform;
        cam.LookAt = p.transform;

        p.SetOnDieListener(() =>
        {
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
