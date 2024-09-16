using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/CueAction")]
public class CueAction : Action
{
    public override void Act(StateController controller)
    {
        BaseEnemyController enemyController = controller.gameObject.GetComponent<BaseEnemyController>();
        enemyController.currentAttack.invokeCue(enemyController);
   

    }
}
