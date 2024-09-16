using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelUpUIController : MonoBehaviour
{
    private WeaponUpgradeManager upgradeManager;
    private Card[] upgradeCards;

    private Card selectedCard;

    public UnityEvent toggleHUDVisiblity;
    public GameObject upgradeText;
    public GameObject futureUpgrades;

    void Start()
    {
        upgradeManager = GetComponent<WeaponUpgradeManager>();
        upgradeCards = GetComponentsInChildren<Card>();
        upgradeText.SetActive(false);
        futureUpgrades.SetActive(false);
        SetupCards();
    }

    void SetupCards()
    {
        System.Random random = new System.Random();
        List<ILevelUpOption> options = upgradeManager.GetLevelUpOptions().OrderBy(x => random.Next()).Take(upgradeCards.Count()).ToList();

        if (options.Count < 1)
        {
            toggleHUDVisiblity.Invoke();
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Levelup");
            Debug.LogWarning("Insufficient upgrades for levelup");
        }
        OnCardSelected(upgradeCards[0], options[0]);
        for (int i = 0; i < upgradeCards.Count(); i++)
        {
            if (i >= options.Count) upgradeCards[i].gameObject.SetActive(false);
            else upgradeCards[i].AssignUpgrade(options[i]);
        }
    }

    public void OnCardSelected(Card card, ILevelUpOption upgrade)
    {
        selectedCard = card;
        if (upgrade is WeaponUpgrade)
        {
            upgradeText.GetComponent<TextMeshProUGUI>().text = "Applies:\n" + upgrade.Description;
            futureUpgrades.GetComponent<TextMeshProUGUI>().text = "Future Upgrades:\n" + upgrade.futureUpgrades;
        }
        else
        {
            Weapon weaponUpgrade = upgrade as Weapon;
            upgradeText.GetComponent<TextMeshProUGUI>().text = "Equip:\n" + weaponUpgrade.Name;
            futureUpgrades.GetComponent<TextMeshProUGUI>().text = "Base Stats:\n" + weaponUpgrade.ShowUpgradeStats();
        }
        upgradeText.SetActive(true);
        futureUpgrades.SetActive(true);
    }

    public void OnConfirm()
    {
        if (selectedCard != null)
        {
            AudioManager.playClip("upgradeConfirm");
            selectedCard.ApplyUpgrade();
            toggleHUDVisiblity.Invoke();

            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("LevelUp");
        }
    }
}