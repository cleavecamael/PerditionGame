using UnityEngine;

[CreateAssetMenu(fileName = "TerrainConstants", menuName = "ScriptableObjects/TerrainConstants", order = 1)]
public class TerrainConstants : ScriptableObject
{
    public float healthPowerUp = 10f;
    public int healthHp = 2;
    public int swarmHp = 3;
    public int ultimateHp = 3;
    public int explosiveHp = 5;
    public int lightingHp = 2;
    public float pickupTime = 0.2f;

    public int timeOut = 15;
    public int respawn = 20;
}
