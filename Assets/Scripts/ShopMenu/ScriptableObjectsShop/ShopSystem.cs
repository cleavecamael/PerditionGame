using UnityEngine;

[CreateAssetMenu(fileName = "ShopSystem", menuName = "ScriptableObjects/ShopSystem")]
public class ShopSystem : ScriptableObject
{

    [Header("Coins Cost")]
    public int costHealth;
    public int costHealth1;
    public int costHealth2;
    public int costHealth3;

    public int costMovement;
    public int costMovement1;
    public int costMovement2;
    public int costMovement3;

    public int costMagnet;
    public int costMagnet1;
    public int costMagnet2;
    public int costMagnet3;


    [Header("Max Level")]
    public int maxLevelHealth = 4;
    public int maxLevelMovement = 4;
    public int maxLevelMagnet = 4;

    [Header("Buff Increase")]
    public int buffHealth;
    public int buffMovement;
    public int buffMagnet;

    [System.Serializable]
    public class ShopDataContainer
    {
        public int levelHealth;
        public int levelMovementSpeed;
        public int levelMagnetDistance;

        public ShopDataContainer()
        {
            levelHealth = 0;
            levelMovementSpeed = 0;
            levelMagnetDistance = 0;
        }
    }

    public ShopDataContainer ShopData { get; set; }

    public void ResetShop()
    {
        ShopData = new ShopDataContainer();
    }

    public void LoadShopData(ShopDataContainer shopData)
    {
        ShopData = shopData;
    }
}
