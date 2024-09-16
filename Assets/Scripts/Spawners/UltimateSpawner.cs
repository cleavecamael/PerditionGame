
using UnityEngine;

public class UltimateSpawner : ItemSpawner
{
    [SerializeField] private XPSystem xPSystem;

    protected override void Start()
    {
        GetBounds();
    }
    public void CheckLevel()
    {
        if (xPSystem.CurrentLevel == xPSystem.CurrentLevelCap-1) BeginSpawn();
    }
}