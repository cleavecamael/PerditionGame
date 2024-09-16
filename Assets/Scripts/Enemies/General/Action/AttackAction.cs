using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/AttackAction")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        BaseEnemyController enemyController = controller.gameObject.GetComponent<BaseEnemyController>();
        enemyController.currentAttack.attack(enemyController);
    }
}
