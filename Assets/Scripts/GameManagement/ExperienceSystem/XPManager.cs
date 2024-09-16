using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class XPManager : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    public UnityEvent toggleHUDVisibility;
    public UnityEvent levelUp;
    public UnityEvent checkBossSpawn;

    public void ResetExperience()
    {
        xpSystem.ResetXP();
    }

    public void AddXP(int amt)
    {
        xpSystem.AddXP(amt);
    }

    public void LaunchLevelUp()
    {
        Time.timeScale = 0;
        toggleHUDVisibility.Invoke();
        levelUp.Invoke();
        AudioManager.playClip("LevelUp");
        SceneManager.LoadScene("LevelUp", LoadSceneMode.Additive);
    }

    public void LevelUpComplete()
    {
        xpSystem.IncreaseLevel();
        checkBossSpawn.Invoke();
    }

    public void IncrementCap()
    {
        xpSystem.IncrementCap();
    }


}