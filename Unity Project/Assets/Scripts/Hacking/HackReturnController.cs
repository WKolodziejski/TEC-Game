using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HackReturnController : MonoBehaviour
{

    public CinemachineVirtualCamera _camera;

    private PlayableDirector _transition;
    
    void Start()
    {
        _transition = GetComponent<PlayableDirector>();
    }

    public void Return(bool won)
    {
        _transition.Play();

        StartCoroutine(IPlay(won));
    }

    private IEnumerator IPlay(bool won)
    {
        yield return new WaitForSeconds(1f * Time.timeScale);

        HackSceneReference.Instance.ReturnHackGame(won);

        _camera.Follow = null;
        _camera.LookAt = null;

        Time.timeScale = 1f;
    }

}
