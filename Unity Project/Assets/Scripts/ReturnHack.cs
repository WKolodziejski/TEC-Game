using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHack : MonoBehaviour
{

    public AudioSource soundtrack;

    private void Awake()
    {
        soundtrack.time = SceneReferences.Instance.GetAudioTime();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
    }
}
