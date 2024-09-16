using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/True")]
public class TrueDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return true;
    }
}