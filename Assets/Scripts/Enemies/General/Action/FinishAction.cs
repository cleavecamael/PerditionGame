using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/FinishAction")]
public class FinishAction : Action
{
    public override void Act(StateController controller)
    {
        BaseEnemyController enemyController = controller.gameObject.GetComponent<BaseEnemyController>();
        enemyController.currentAttack.invokeFinish(enemyController);
    }
}
