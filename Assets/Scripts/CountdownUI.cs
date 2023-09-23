using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Drag your Countdown Text component here in the Inspector
    public TextMeshProUGUI timerText;     // Drag your Timer Text component here in the Inspector
    public int countdownNumber = 3; // Starting from 3
    public float survivalTime = 0f; // Player's survival time

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
            yield return new WaitForSecondsRealtime(1f);
            countdownNumber--; // Decrease the countdown number
        }

        // Display "GO!" once the countdown is complete
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f); // Show "GO!" for 1 second
        countdownText.gameObject.SetActive(false); // Hide the countdown text after showing "GO!"

        StartGame();
    }

    void Update()
    {
        if (Time.timeScale == 1) // If the game is running
        {
            survivalTime += Time.deltaTime; // Increment the timer by the time since last frame
            DisplayTime(); // Update the timer display
        }
    }

    void DisplayTime()
    {
        // Convert the survival time into minutes and seconds
        int minutes = Mathf.FloorToInt(survivalTime / 60F);
        int seconds = Mathf.FloorToInt(survivalTime - minutes * 60);
        
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds); // Update the timer text
    }

    void StartGame()
    {
        // Unpause the game
        Time.timeScale = 1;
        Debug.Log("Game Started!");
    }
}
