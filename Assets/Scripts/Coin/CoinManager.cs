using UnityEngine;
public class CoinManager : MonoBehaviour
{
    public CoinSystem coinSystem;
    public GameObject coin;
    public int minCoins;
    public int maxCoins;
    public void OnSpawnCoin(Vector2 result)
    {
        for (int i = 0; i < Random.Range(minCoins, maxCoins); i++)
        {
            Instantiate(coin, result, Quaternion.identity);
        }
    }
    public void OnCollectCoin(float val)
    {
        AudioManager.playClip(GetComponent<AudioSource>(), "soulPickup");
        coinSystem.AddCoin(val);
    }
}