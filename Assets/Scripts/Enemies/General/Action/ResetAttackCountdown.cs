using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/ResetAttackTimer")]
public class ResetAttackCountdown : Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<BaseEnemyController>().resetLastAttacked();
    
    }
}
