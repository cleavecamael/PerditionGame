using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 7)]
public class GameConstants : ScriptableObject
{
    // For timer
    public float startingOxygenTime;

    // For fever meter
    public float maxFever;
    public float feverDecay;
    public float feverFillSpeed;

    // for xp bar
    public float xpFillSpeed;

    // For HP bar
    public float hpFillSpeed;

    // For inventory
    public int maxInventorySize;

}