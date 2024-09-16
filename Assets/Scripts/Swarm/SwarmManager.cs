using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    public Vector3Position player;
    public GameObject swarmObject;

    public void OnSpawn()
    {
        Instantiate(swarmObject, player.pos, Quaternion.identity);
    }

}
