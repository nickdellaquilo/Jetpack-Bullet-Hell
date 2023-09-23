using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Drag your TextMeshProUGUI component here in the Inspector
    private int countdownNumber = 3; // Starting from 3

    void Start()
    {
        // Ensure the game is paused at the start
        Time.timeScale = 0;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (countdownNumber > 0)
        {
            // Update the text to show the current countdown number
            countdownText.text = countdownNumber.ToString();

            // Use real-time delay because timeScale is 0
            yield return new WaitForSecondsRealtime(1f);

            countdownNumber--; // Decrease the countdown number
        }

        // Display "GO!" once the countdown is complete
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f); // Show "GO!" for 1 second

        StartGame();

        countdownText.gameObject.SetActive(false); // Hide the countdown text after showing "GO!"
    }

    void StartGame()
    {
        // Unpause the game
        Time.timeScale = 1;
        Debug.Log("Game Started!");
    }
}

