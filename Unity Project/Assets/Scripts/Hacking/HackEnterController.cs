using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static HackSceneReference;

public class HackEnterController : MonoBehaviour
{

    public CinemachineVirtualCamera _camera;

    private PlayableDirector _transition;

    void Start()
    {
        _transition = GetComponent<PlayableDirector>();
    }

    public void Enter(GameObject target, EDifficulty difficulty)
    {
        _camera.LookAt = target.transform;
        _camera.Follow = target.transform;
        _transition.Play();

        StartCoroutine(IPlay(difficulty));
    }

    private IEnumerator IPlay(EDifficulty difficulty)
    {
        yield return new WaitForSeconds(1.5f);

        HackSceneReference.Instance.EnterHackGame(difficulty);
    }

}
