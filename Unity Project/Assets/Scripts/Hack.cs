using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hack : MonoBehaviour
{

    void Awake()
    {
        SceneReferences.Instance.ReloadObjects();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            SceneReferences.Instance.PersistObjects();

            SceneManager.LoadScene(1);
        }
    }

}
