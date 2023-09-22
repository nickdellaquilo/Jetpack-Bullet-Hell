using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    private static GameBGM instance = null;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RestartMusic(){
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
    }

}
