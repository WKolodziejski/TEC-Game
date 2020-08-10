using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour
{

    public void Pause()
    {
        Time.timeScale = 0f;

        FindObjectOfType<GameController>().isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        FindObjectOfType<GameController>().isPaused = false;
        gameObject.SetActive(false);
    }

    public void Options()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
