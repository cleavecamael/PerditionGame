using UnityEngine;
using UnityEngine.Events;

public class Initializer : MonoBehaviour
{
    [SerializeField] private CurrentMap currentMap;
    [SerializeField] private AllWeaponUpgrades allWeaponUpgrades;
    [SerializeField] private UnityEvent startGame;
    [SerializeField] private UnityEvent newWorld;
    

    [Header("For testing")]
    public bool independentScene;

    void Awake()
    {
        if (currentMap.CurrentMapName == "World-1" || independentScene)
        {
            allWeaponUpgrades.ResetOptions();
            startGame.Invoke();
        }
        else
        {
            newWorld.Invoke();
        }
        Time.timeScale = 1;
    }
}