using UnityEngine;

public class SwarmTerrainController : BaseInteractiveTerrainController
{

    public override void SpawnPowerup()
    {
        GameObject x = Instantiate(base.powerUp, transform.position, Quaternion.identity);
    }
    void Start()
    {
        currentHp = terrainConstants.swarmHp;
        maxHp = terrainConstants.swarmHp;
    }
}