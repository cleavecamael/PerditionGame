using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{

    public PlayerStats playerStats;
    public HealthSystem healthSystem;
    public XPSystem xPSystem;
    public Inventory inventory;
    public void OnGameOver()
    {
        inventory.ResetInventory();
        SceneManager.LoadScene("GameOver");
    }

    public void OnGameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
