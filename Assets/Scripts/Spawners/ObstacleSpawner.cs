using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] float xSpread;
    [SerializeField] float ySpread;
    [SerializeField] int numToSpawn;
    [SerializeField] int minOffset;
    [SerializeField] private int maxAttempts;
    Vector3 posToSpawn;
    Vector3 bounds;

    void Start()
    {
        GetBounds();
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            bool pos = FindPos();
            if (pos)
            {
                Instantiate(item, posToSpawn, Quaternion.identity, transform);
                return;
            }
        }
    }

    bool FindPos()
    {
        int failCount = 0;
        posToSpawn = new Vector3(Random.Range(-xSpread, xSpread), Random.Range(-ySpread, ySpread), 0) + transform.position;
        Collider2D[] res = new Collider2D[1];
        // TODO: add layermask eventually 
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Obstacles"));
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Default"));
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Water"));
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Player"));
        if (failCount == 0) return true;
        return false;
    }

    void GetBounds()
    {
        GameObject ins = Instantiate(item);
        ins.GetComponentInChildren<Tilemap>().CompressBounds();
        bounds = ins.GetComponentInChildren<Tilemap>().localBounds.size;
        bounds.x += minOffset;
        bounds.y += minOffset;
        Destroy(ins);
    }

}