using UnityEngine;

public abstract class WeaponUpgrade : ScriptableObject, ILevelUpOption
{
    [Header("Details")]
    [SerializeField] protected string _upgradeName;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _futureUpgrades;
    public Weapon weapon;
    public WeaponUpgrade prerequisite; // Prerequisite upgrade for this one

    public string Name
    {
        get
        {
            return _upgradeName;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
    }
    public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }

    public string futureUpgrades
    {
        get
        {
            return _futureUpgrades;
        }
    }

    public virtual void Apply()
    {
        weapon.AddUpgrade(this);
    }
}
