using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    private static GameBGM instance = null;
    public AudioSource bgm;
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void RestartMusic(){
        bgm.Stop();
        bgm.Play();
    }

}
