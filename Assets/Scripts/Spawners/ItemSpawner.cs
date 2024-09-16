using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] float xSpread;
    [SerializeField] float ySpread;
    [SerializeField] int numToSpawn;
    [SerializeField] int minOffset;
    [SerializeField] private int maxAttempts;
    Vector3 posToSpawn;
    Vector3 bounds;

    protected virtual void Start()
    {
        GetBounds();
        BeginSpawn();
    }
    
    protected void BeginSpawn()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
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
        posToSpawn = new Vector3(UnityEngine.Random.Range(-xSpread, xSpread), UnityEngine.Random.Range(-ySpread, ySpread), 0) + transform.position;
        Collider2D[] res = new Collider2D[1];
        // TODO: add layermask eventually 
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Default"));
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Obstacles"));
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Water"));
        failCount += Physics2D.OverlapBoxNonAlloc(posToSpawn, bounds, 0f, res, 1 << LayerMask.NameToLayer("Player"));
        if (failCount == 0) return true;
        return false;
    }

    protected void GetBounds()
    {
        bounds = item.GetComponent<SpriteRenderer>().bounds.size;
        bounds.x += minOffset;
        bounds.y += minOffset;
    }
}