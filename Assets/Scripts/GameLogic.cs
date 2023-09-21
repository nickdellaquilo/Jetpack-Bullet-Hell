using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    bool gameStart  = true;
    bool gameEnd    = false;

    int currScore   = 0;
    int highScore   = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // High score update
        if (currScore > highScore)
        {
            highScore = currScore;
        }

        
    }
}
