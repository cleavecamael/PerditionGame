using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/DecisionTemplate")]
public class NextAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}