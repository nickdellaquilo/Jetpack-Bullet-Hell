using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float survivalTime;
    private float enemiesKilled;

    public void Awake()
    {
        survivalTime = PlayerPrefs.GetFloat("Time");
        Debug.Log(survivalTime);
        int minutes = Mathf.FloorToInt(survivalTime / 60F);
        int seconds = Mathf.FloorToInt(survivalTime - minutes * 60);
        scoreText.text = "Score: " + string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void Restart()
    {
        GameBGM bgm = FindObjectOfType<GameBGM>();
        if(bgm)
        {
            bgm.RestartMusic();
        }
        SceneManager.LoadScene(1);
    }
}
