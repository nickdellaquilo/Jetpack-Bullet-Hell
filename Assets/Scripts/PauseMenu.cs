using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settings;
    public GameObject controls;
    public GameObject[] gameObjects;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (settings.activeSelf || controls.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
            foreach (GameObject go in gameObjects)
            {
                go.SetActive(false);
            }
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        controls.SetActive(false);
        Time.timeScale = 1f;
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(true);
        }
    }

    public void LoadSettings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void LoadControls()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void GoBack()
    {
        pauseMenu.SetActive(true);
        settings.SetActive(false);
        controls.SetActive(false);
    }

}
