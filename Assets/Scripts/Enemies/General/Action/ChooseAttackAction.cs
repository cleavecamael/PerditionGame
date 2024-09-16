using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ChooseAttackAction")]
public class ChooseAttackAction : Action
{
    public override void Act(StateController controller)
    {
        
        BaseEnemyController enemyController = controller.gameObject.GetComponent<BaseEnemyController>();
        BaseEnemyAttacks enemyAttacks = controller.gameObject.GetComponent<BaseEnemyAttacks>();
        EnemyAttack chosenAttack = enemyAttacks.chooseAttack();

        enemyController.currentAttackName = chosenAttack.attackName;
        if (chosenAttack.attackName.Equals("null"))
        {
            enemyController.currentAttack = null ;
        }
        else
        {
            enemyController.currentAttack = chosenAttack;
          
        }
       
    }
}
