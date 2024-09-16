using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    public GameConstants gameConstants;
    float remainingTime;
    public OxygenTimeSystem oxygenTimeSystem;
    TextMeshProUGUI timerText;
    bool gameOver = false;

    // private bool oxygenRunOutEventCalled = false;
    // public UnityEvent<bool> oxygenRunOutEvent;

    public void OnGameRestart()
    {
        gameOver = false;
        // oxygenRunOutEvent.Invoke(false);
        // oxygenRunOutEventCalled = false;
    }
    public void OnGameOver()
    {
        gameOver = true;
        oxygenTimeSystem.currentTime = remainingTime;
    }

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "World-1")
            oxygenTimeSystem.currentTime = 0;
        remainingTime = oxygenTimeSystem.currentTime;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }
        remainingTime += Time.deltaTime;
        oxygenTimeSystem.currentTime = remainingTime;
        // else
        // {
        //     remainingTime = 0;
        //     if (!oxygenRunOutEventCalled)
        //     {
        //         oxygenRunOutEvent.Invoke(true);
        //         oxygenRunOutEventCalled = true;
        //     }
        // }

        int min = Mathf.FloorToInt(remainingTime / 60);
        int sec = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", min, sec);

    }
}