using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PauseUIManager : MonoBehaviour
{

    public PlayerStats playerStats;
    public UnityEvent pauseEvent;
    public CoinSystem coinSystem;
    public KillCountSystem killCountSystem;
    public OxygenTimeSystem oxygenTimeSystem;
    [SerializeField] private UnityEvent quitGameFromPause;

    void Awake()
    {
        int min = Mathf.FloorToInt(oxygenTimeSystem.currentTime / 60);
        int sec = Mathf.FloorToInt(oxygenTimeSystem.currentTime % 60);
        GameObject.Find("PlayerMaxHealthText").GetComponent<TextMeshProUGUI>().text = "Health: " + playerStats.health;
        GameObject.Find("PlayerMagnetText").GetComponent<TextMeshProUGUI>().text = "Magnet: " + playerStats.magnetDistance;
        GameObject.Find("PlayerMovementSpeedText").GetComponent<TextMeshProUGUI>().text = "Movement: " + playerStats.movementSpeed;
        GameObject.Find("TotalCoinsText").GetComponent<TextMeshProUGUI>().text = "Gold: " + coinSystem.CoinData.coinTotal;
        GameObject.Find("KillCounterText").GetComponent<TextMeshProUGUI>().text = "Kills: " + killCountSystem.count;
        GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Time: " + string.Format("{0:00}:{1:00}", min, sec);

    }

    public void OnResume()
    {
        pauseEvent.Invoke();
    }

    public void MainMenu()
    {
        quitGameFromPause.Invoke();
        SceneManager.LoadScene("Main Menu 2");
    }

}
