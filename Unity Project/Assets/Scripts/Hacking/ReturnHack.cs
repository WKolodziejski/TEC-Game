using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHack : MonoBehaviour
{

    //public AudioSource soundtrack;

    private void Awake()
    {
        //soundtrack.time = SceneReferences.Instance.GetAudioTime();
    }

    void Update()
    {
        
    }

    public void exitHack()
    {
        //SceneManager.UnloadScene();
        SceneManager.LoadScene(0);
    }
}
