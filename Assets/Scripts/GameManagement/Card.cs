using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [Header("Fields")]
    public TextMeshProUGUI upgradeName;
    public Image upgradeImage;

    public Image upgradePath;

    [Header("Options")]
    private ILevelUpOption upgrade;
    private LevelUpUIController levelUpUIController;

    [Header("Events")]
    public UnityEvent<Weapon> addWeapon;
    public UnityEvent<WeaponUpgrade> upgradeSelected;

    void Awake()
    {
        levelUpUIController = GetComponentInParent<LevelUpUIController>();
    }

    public void OnSelected()
    {

        levelUpUIController.OnCardSelected(this, upgrade);
    }

    public void AssignUpgrade(ILevelUpOption option)
    {

        upgrade = option;
        upgradeName.text = upgrade.Name;
        upgradeImage.sprite = upgrade.Icon;
    }

    public void ApplyUpgrade()
    {
        if (upgrade is Weapon weapon)
        {
            addWeapon.Invoke(weapon);
        }
        else if (upgrade is WeaponUpgrade weaponUpgrade)
        {
            upgradeSelected.Invoke(weaponUpgrade);
            weaponUpgrade.Apply();
        }
    }
}