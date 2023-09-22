using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void Controls()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
}
