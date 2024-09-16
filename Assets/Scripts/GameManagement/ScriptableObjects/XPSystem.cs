using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/XPSystem")]
public class XPSystem : ScriptableObject
{
    public SimpleGameEvent levelUp;

    [Header("World progression")]
    [Tooltip("The level at which the boss will spawn")]
    [SerializeField] private int levelCap;
    [Tooltip("Killing boss will increase cap by this amount")]
    [SerializeField] private int capIncrement;

    [Header("XP Balancing")]
    public int[] xpToLevel;

    public int CurrentLevelCap { get; private set; }
    public int ReqXPForCurrentLevel
    {
        get
        {
            if (CurrentLevel - 1 < xpToLevel.Length && CurrentLevel < CurrentLevelCap) return xpToLevel[CurrentLevel - 1];
            else return -1;
        }
    }

    public int ReqXPForPreviousLevel
    {
        get
        {
            if (CurrentLevel - 1 < xpToLevel.Length && CurrentLevel > 1) return xpToLevel[CurrentLevel - 2];
            else return 0;
        }
    }

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentXP { get; private set; } = 0;

    public void AddXP(int amt)
    {
        if (CurrentXP < ReqXPForCurrentLevel)
        {
            CurrentXP += amt;
            CheckLevellingConditions();
        }
    }

    void CheckLevellingConditions()
    {
        if (CurrentLevel - 1 < xpToLevel.Length && CurrentLevel <= CurrentLevelCap && CurrentXP >= ReqXPForCurrentLevel)
        {
            levelUp.Raise(null);
        }
    }
    public void IncreaseLevel()
    {
        CurrentXP = ReqXPForCurrentLevel;
        CurrentLevel++;
    }
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    // on boss kill
    public void IncrementCap()
    {
        CurrentLevelCap += capIncrement;
    }

    public void ResetXP()
    {
        CurrentLevel = 1;
        CurrentXP = 0;
        CurrentLevelCap = levelCap;
    }

}
