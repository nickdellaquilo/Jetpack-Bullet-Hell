using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
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
