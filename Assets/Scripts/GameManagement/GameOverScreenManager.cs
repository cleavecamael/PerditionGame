using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour
{

    public OxygenTimeSystem oxygenTimeSystem;
    public XPSystem xPSystem;
    public KillCountSystem KillCountSystem;
    public CoinSystem coinSystem;
    public CurrentMap currentMap;
    public HealthSystem healthSystem;
    public PlayerStats playerStats;

    void Start()
    {
        int min = Mathf.FloorToInt(oxygenTimeSystem.currentTime / 60);
        int sec = Mathf.FloorToInt(oxygenTimeSystem.currentTime % 60);
        GameObject.Find("TimeOxygen").GetComponent<TextMeshProUGUI>().text = "Time Taken: " + string.Format("{0:00}:{1:00}", min, sec);
        GameObject.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level: " + xPSystem.CurrentLevel;
        GameObject.Find("Kills").GetComponent<TextMeshProUGUI>().text = "Kills: " + KillCountSystem.count;
        GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = "Gold: " + coinSystem.CoinData.coinTotal;
    }

    public void onRestart()
    {
        healthSystem.SetHealth(playerStats.health);
        coinSystem.ResetCoinsEarned();
        SceneManager.LoadScene("World-1");
    }
    public void onMenu()
    {
        SceneManager.LoadScene("Main Menu 2");
    }
}