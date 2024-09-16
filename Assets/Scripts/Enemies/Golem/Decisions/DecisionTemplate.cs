using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/DecisionTemplate")]
public class DecisionTemplate1 : Decision
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}