using UnityEngine;

[CreateAssetMenu(fileName = "CoinSystem", menuName = "ScriptableObjects/CoinSystem")]
public class CoinSystem : ScriptableObject
{
    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    [System.Serializable]
    public class CoinDataContainer
    {
        public float coinEarned;
        public float coinTotal;
        public float coinsSpent;

        public CoinDataContainer()
        {
            coinEarned = 0;
            coinTotal = 0;
            coinsSpent = 0;
        }
    }

    public CoinDataContainer CoinData { get; private set; }

    public void ResetAllCoins()
    {
        CoinData = new CoinDataContainer();
    }

    public void RefundCoins()
    {
        CoinData.coinTotal += CoinData.coinsSpent;
        CoinData.coinsSpent = 0;
    }

    public void LoadCoinData(CoinDataContainer coinData)
    {
        CoinData = coinData;
    }

    public void ResetCoinsEarned()
    {
        CoinData.coinEarned = 0;
    }

    public void AddCoin(float addedValue)
    {
        CoinData.coinTotal += addedValue;
        CoinData.coinEarned += addedValue;
    }
    public void MinusCoin(float minusValue)
    {
        CoinData.coinTotal -= minusValue;
        if (CoinData.coinTotal < 0)
        {
            CoinData.coinTotal = 0;
        }
    }

}
