using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCountManager : MonoBehaviour
{
    public KillCountSystem killCountSystem;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "World-1")
            killCountSystem.ResetKillCount();
    }

    public void OnKill()
    {
        killCountSystem.AddKillCount();
    }
    public void OnRestart()
    {
        killCountSystem.ResetKillCount();
    }
}
