using UnityEngine;

public interface ILevelUpOption
{
    public string Name { get; }
    public string Description { get; }
    public string futureUpgrades { get; }
    public Sprite Icon { get; }

}