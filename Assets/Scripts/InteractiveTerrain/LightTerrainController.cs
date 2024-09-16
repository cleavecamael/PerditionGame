using UnityEngine;

public class LightTerrainController : BaseInteractiveTerrainController
{
    public override void SpawnPowerup()
    {
        GameObject x = Instantiate(base.powerUp, transform.position + Vector3.down * base.powerUp.GetComponent<SpriteRenderer>().size.y, Quaternion.identity);
    }

    void Start()
    {
        currentHp = terrainConstants.swarmHp;
        maxHp = terrainConstants.swarmHp;
    }

}