using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/DragonDecision")]
public class AttackChoiceDecision : Decision
{
    public string attack;
    public override bool Decide(StateController controller)
    {
        return attack.Equals(controller.GetComponent<DragonController>().attackState);
    }
}