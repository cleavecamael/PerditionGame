using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private int minSouls;
    [SerializeField] private int maxSouls;

    public void SpawnSoul(Vector3 pos, int exp)
    {
        int nSouls = Random.Range(minSouls, maxSouls);
        for (int i = 0; i < nSouls; i++)
        {
            var s = Instantiate(soulPrefab, pos, Quaternion.identity, transform);
            SoulController soul = s.GetComponent<SoulController>();
            soul.SetXP(Mathf.RoundToInt(exp/nSouls));
        }
    }
}