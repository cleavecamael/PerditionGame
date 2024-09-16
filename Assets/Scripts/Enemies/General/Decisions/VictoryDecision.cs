using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/VictoryDecision")]
public class VictoryDecision : Decision
{
    public override bool Decide(StateController controller)
    {
       return controller.gameObject.GetComponent<BaseEnemyController>().victory;
    }
}