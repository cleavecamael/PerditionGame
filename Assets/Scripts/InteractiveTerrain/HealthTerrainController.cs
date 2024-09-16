using UnityEngine;

public class HealthTerrainController : BaseInteractiveTerrainController
{
    public override void SpawnPowerup()
    {
        GameObject x = Instantiate(base.powerUp, transform.position, Quaternion.identity);
    }
    void Start()
    {
        currentHp = terrainConstants.healthHp;
        maxHp = terrainConstants.healthHp;
    }


}