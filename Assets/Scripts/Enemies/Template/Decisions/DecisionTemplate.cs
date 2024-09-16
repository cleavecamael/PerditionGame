using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/DecisionTemplate")]
public class DecisionTemplate : Decision
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}