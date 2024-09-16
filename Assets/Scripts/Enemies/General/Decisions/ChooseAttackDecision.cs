using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/ChooseAttackDecision")]
public class ChooseAttackDecision : Decision
{
    public string attack;
    public override bool Decide(StateController controller)
    {
        return attack.Equals(controller.GetComponent<BaseEnemyController>().currentAttackName);
     
    }
}