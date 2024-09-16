using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/DecisionTemplate")]
public class BatDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}