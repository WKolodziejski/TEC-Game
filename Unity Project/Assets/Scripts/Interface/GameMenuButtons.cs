using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour
{

    public AudioReverbFilter reverb;

    public void Pause()
    {
        reverb.enabled = true;

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;

        GameController.isPaused = true;
    }

    public void Resume()
    {
        reverb.enabled = false;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        GameController.isPaused = false;
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
