using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize;
    private Queue<GameObject> pool;
    private GameObject bulletStorage;

    void Awake()
    {
        bulletStorage = GameObject.FindWithTag("BulletStorage");
        if (bulletStorage == null) Debug.LogError("Bullet storage doesn't exist");

        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, bulletStorage.transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetBullet(Vector3 pos)
    {
        GameObject bullet = pool.Dequeue();

        bullet.SetActive(true);
        bullet.transform.position = pos;

        pool.Enqueue(bullet);

        return bullet;
    }
}