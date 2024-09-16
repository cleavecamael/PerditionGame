using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    public ShopSystem shop;
    public CoinSystem coinSystem;
    private int coins;
    private int magnet;
    private int movementSpeed;
    private int health;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private CurrentMap currentMap;
    [SerializeField] private UnityEvent initializeShop;
    [SerializeField] private UnityEvent resaveCoins;
    [SerializeField] private UnityEvent saveShopSystem;

    void Awake()
    {
        initializeShop.Invoke();
    }

    void Start()
    {
        health = shop.ShopData.levelHealth;
        magnet = shop.ShopData.levelMagnetDistance;
        movementSpeed = shop.ShopData.levelMovementSpeed;
        coins = (int)coinSystem.CoinData.coinTotal;
        CoinsUpdate();
        FillDescription();
        IndicateLevel("Health", shop.ShopData.levelHealth, shop.maxLevelHealth);
        IndicateLevel("Movement", shop.ShopData.levelMovementSpeed, shop.maxLevelMovement);
        IndicateLevel("Magnet", shop.ShopData.levelMagnetDistance, shop.maxLevelMagnet);
        // OnDebugLog();

    }

    void OnDebugLog()
    {
        Debug.Log("shop health " + shop.ShopData.levelHealth);
        Debug.Log("shop movement " + shop.ShopData.levelMovementSpeed);
        Debug.Log("shop level magnet " + shop.ShopData.levelMagnetDistance);
    }

    void CoinsUpdate()
    {
        resaveCoins.Invoke();
        GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = "" + coins;
        if (shop.ShopData.levelHealth == 0)
            GameObject.Find("HealthPrice").GetComponent<TextMeshProUGUI>().text = shop.costHealth + " Gold";
        else if (shop.ShopData.levelHealth == 1)
            GameObject.Find("HealthPrice").GetComponent<TextMeshProUGUI>().text = shop.costHealth1 + " Gold";
        else if (shop.ShopData.levelHealth == 2)
            GameObject.Find("HealthPrice").GetComponent<TextMeshProUGUI>().text = shop.costHealth2 + " Gold";
        else if (shop.ShopData.levelHealth == 3)
            GameObject.Find("HealthPrice").GetComponent<TextMeshProUGUI>().text = shop.costHealth3 + " Gold";
        else if (shop.ShopData.levelHealth == 4)
            GameObject.Find("HealthPrice").GetComponent<TextMeshProUGUI>().text = "MAX";
        if (shop.ShopData.levelMovementSpeed == 0)
            GameObject.Find("MovementPrice").GetComponent<TextMeshProUGUI>().text = shop.costMovement + " Gold";
        else if (shop.ShopData.levelMovementSpeed == 1)
            GameObject.Find("MovementPrice").GetComponent<TextMeshProUGUI>().text = shop.costMovement1 + " Gold";
        else if (shop.ShopData.levelMovementSpeed == 2)
            GameObject.Find("MovementPrice").GetComponent<TextMeshProUGUI>().text = shop.costMovement2 + " Gold";
        else if (shop.ShopData.levelMovementSpeed == 3)
            GameObject.Find("MovementPrice").GetComponent<TextMeshProUGUI>().text = shop.costMovement3 + " Gold";
        else if (shop.ShopData.levelMovementSpeed == 4)
            GameObject.Find("MovementPrice").GetComponent<TextMeshProUGUI>().text = "MAX";
        if (shop.ShopData.levelMagnetDistance == 0)
            GameObject.Find("MagnetPrice").GetComponent<TextMeshProUGUI>().text = shop.costMagnet + " Gold";
        else if (shop.ShopData.levelMagnetDistance == 1)
            GameObject.Find("MagnetPrice").GetComponent<TextMeshProUGUI>().text = shop.costMagnet1 + " Gold";
        else if (shop.ShopData.levelMagnetDistance == 2)
            GameObject.Find("MagnetPrice").GetComponent<TextMeshProUGUI>().text = shop.costMagnet2 + " Gold";
        else if (shop.ShopData.levelMagnetDistance == 3)
            GameObject.Find("MagnetPrice").GetComponent<TextMeshProUGUI>().text = shop.costMagnet3 + " Gold";
        else if (shop.ShopData.levelMagnetDistance == 4)
            GameObject.Find("MagnetPrice").GetComponent<TextMeshProUGUI>().text = "MAX";
    }

    void FillDescription()
    {
        string[] stats = new string[] { "Health", "Movement", "Magnet" };

        foreach (var stat in stats)
        {
            TextMeshProUGUI desc = GameObject.Find($"{stat}Description").GetComponent<TextMeshProUGUI>();

            switch (stat)
            {
                case "Health":
                    desc.text = $"Max {stat} + {shop.buffHealth}";
                    break;

                case "Movement":
                    desc.text = $"Max {stat} + {shop.buffMovement}";
                    break;

                case "Magnet":
                    desc.text = $"Max {stat} + {shop.buffMagnet}";
                    break;
            }
        }


    }
    void IndicateLevel(String statName, int levelTarget, int maxLevel)
    {
        GameObject mainElement = GameObject.Find(statName).gameObject;
        GameObject currentVal = mainElement.transform.Find("CurrentVal").gameObject;
        if (statName == "Health")
            currentVal.GetComponent<TextMeshProUGUI>().text = statName + ": " + (health * shop.buffHealth + playerStats.baseHealth);
        else if (statName == "Movement")
            currentVal.GetComponent<TextMeshProUGUI>().text = statName + ": " + (movementSpeed * shop.buffMovement + playerStats.baseMovementSpeed);
        else if (statName == "Magnet")
            currentVal.GetComponent<TextMeshProUGUI>().text = statName + ": " + (magnet * shop.buffMagnet + playerStats.baseMagnetDistance);

        mainElement = mainElement.transform.Find("Levels").gameObject;

        // Debug.Log(mainElement.name);
        for (int i = 1; i <= maxLevel; i++)
        {
            string level = "Level-" + i;
            var boundaryBox = mainElement.transform.Find(level);
            // Debug.Log(level);
            if (i <= levelTarget)
            {
                boundaryBox.transform.Find("Check").GetComponent<Image>().enabled = true;
            }
            else
            {
                boundaryBox.transform.Find("Check").GetComponent<Image>().enabled = false;
            }
        }
        // OnDebugLog();
    }
    public void OnClickHealth()
    {
        Debug.Log("Element Health");
        if (health < shop.maxLevelHealth)
        {
            if (shop.ShopData.levelHealth == 0 && coins >= shop.costHealth)
                coins -= shop.costHealth;
            else if (shop.ShopData.levelHealth == 1 && coins >= shop.costHealth1)
                coins -= shop.costHealth1;
            else if (shop.ShopData.levelHealth == 2 && coins >= shop.costHealth2)
                coins -= shop.costHealth2;
            else if (shop.ShopData.levelHealth == 3 && coins >= shop.costHealth3)
                coins -= shop.costHealth3;
            else
                return;
            coinSystem.CoinData.coinsSpent += coinSystem.CoinData.coinTotal - coins;
            coinSystem.CoinData.coinTotal = coins;
            health += 1;
            shop.ShopData.levelHealth = health;
            saveShopSystem.Invoke();
            playerStats.health = playerStats.baseHealth + health * shop.buffHealth;
            CoinsUpdate();
            IndicateLevel("Health", shop.ShopData.levelHealth, shop.maxLevelHealth);
        }
    }

    public void OnClickMovementSpeed()
    {
        if (shop.ShopData.levelMovementSpeed < shop.maxLevelMovement)
        {
            if (shop.ShopData.levelMovementSpeed == 0 && coins >= shop.costMovement)
                coins -= shop.costMovement;
            else if (shop.ShopData.levelMovementSpeed == 1 && coins >= shop.costMovement1)
                coins -= shop.costMovement1;
            else if (shop.ShopData.levelMovementSpeed == 2 && coins >= shop.costMovement2)
                coins -= shop.costMovement2;
            else if (shop.ShopData.levelMovementSpeed == 3 && coins >= shop.costMovement3)
                coins -= shop.costMovement3;
            else
                return;
            coinSystem.CoinData.coinsSpent += coinSystem.CoinData.coinTotal - coins;
            coinSystem.CoinData.coinTotal = coins;
            movementSpeed += 1;
            shop.ShopData.levelMovementSpeed = movementSpeed;
            saveShopSystem.Invoke();
            CoinsUpdate();
            playerStats.movementSpeed = playerStats.baseMovementSpeed + movementSpeed * shop.buffMovement;
            IndicateLevel("Movement", shop.ShopData.levelMovementSpeed, shop.maxLevelMovement);
        }
    }
    public void OnClickMagnet()
    {
        if (shop.ShopData.levelMagnetDistance < shop.maxLevelMagnet)
        {
            if (shop.ShopData.levelMagnetDistance == 0 && coins >= shop.costMagnet)
                coins -= shop.costMagnet;
            else if (shop.ShopData.levelMagnetDistance == 1 && coins >= shop.costMagnet1)
                coins -= shop.costMagnet1;
            else if (shop.ShopData.levelMagnetDistance == 2 && coins >= shop.costMagnet2)
                coins -= shop.costMagnet2;
            else if (shop.ShopData.levelMagnetDistance == 3 && coins >= shop.costMagnet3)
                coins -= shop.costMagnet3;
            else
                return;
            coinSystem.CoinData.coinsSpent += coinSystem.CoinData.coinTotal - coins;
            coinSystem.CoinData.coinTotal = coins;
            magnet += 1;
            shop.ShopData.levelMagnetDistance = magnet;
            saveShopSystem.Invoke();
            CoinsUpdate();
            playerStats.magnetDistance = playerStats.baseMagnetDistance + magnet * shop.buffMagnet;
            IndicateLevel("Magnet", shop.ShopData.levelMagnetDistance, shop.maxLevelMagnet);
        }
    }

    public void OnResetShop()
    {
        playerStats.ResetPlayerStats();
        shop.ResetShop();
        coinSystem.RefundCoins();
        coins = (int)coinSystem.CoinData.coinTotal;
        CoinsUpdate();
        saveShopSystem.Invoke();
        health = shop.ShopData.levelHealth;
        magnet = shop.ShopData.levelMagnetDistance;
        movementSpeed = shop.ShopData.levelMovementSpeed;
        IndicateLevel("Health", shop.ShopData.levelHealth, shop.maxLevelHealth);
        IndicateLevel("Movement", shop.ShopData.levelMovementSpeed, shop.maxLevelMovement);
        IndicateLevel("Magnet", shop.ShopData.levelMagnetDistance, shop.maxLevelMagnet);
    }



    public void OnMainMenu()
    {
        SceneManager.LoadScene("Main Menu 2");
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(currentMap.nextMap[currentMap.CurrentMapName]);
    }
}
