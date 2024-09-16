using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private string coinFilepath;

    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] private string shopFilepath;

    public void SaveCoins()
    {
        string coinsSave = JsonUtility.ToJson(coinSystem.CoinData);
        FileHandler.Save(coinFilepath, coinsSave);
    }

    public void LoadCoins()
    {
        if (FileHandler.CheckFileExists(coinFilepath))
        {
            string coins = FileHandler.Load(coinFilepath);
            coinSystem.LoadCoinData(JsonUtility.FromJson<CoinSystem.CoinDataContainer>(coins));
        }
        else
        {
            coinSystem.ResetAllCoins();
            SaveCoins();
        }
    }

    public void SaveShop()
    {
        string shopSave = JsonUtility.ToJson(shopSystem.ShopData);
        FileHandler.Save(shopFilepath, shopSave);
    }

    public void LoadShop()
    {
        if (FileHandler.CheckFileExists(shopFilepath))
        {
            string shop = FileHandler.Load(shopFilepath);
            shopSystem.LoadShopData(JsonUtility.FromJson<ShopSystem.ShopDataContainer>(shop));
        }
        else
        {
            shopSystem.ResetShop();
            SaveShop();
        }
    }
}