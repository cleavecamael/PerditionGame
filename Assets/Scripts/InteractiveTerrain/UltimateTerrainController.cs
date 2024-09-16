using UnityEngine;

public class UltimateTerrainController : BaseInteractiveTerrainController
{
    public override void SpawnPowerup()
    {
        GameObject x = Instantiate(base.powerUp, transform.position, Quaternion.identity);
    }

    void Start()
    {
        currentHp = terrainConstants.ultimateHp;
        maxHp = terrainConstants.ultimateHp;
    }
}