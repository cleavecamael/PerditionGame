using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SwarmParent : MonoBehaviour
{
    public Vector3Position player;

    float duration = 10f;

    public UnityEvent deSpawn;

    void Start()
    {
        StartCoroutine(TimeDie());
    }

    void Update()
    {
        transform.position = player.pos;
    }

    IEnumerator TimeDie()
    {
        yield return new WaitForSecondsRealtime(duration);
        deSpawn.Invoke();
        Destroy(gameObject);
    }
}
