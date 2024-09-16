using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/AttackCooldown")]
public class AttackCooldown : Decision
{
    public float cooldown;
    public override bool Decide(StateController controller)
    {
        BaseEnemyController enemyController = controller.GetComponent<BaseEnemyController>();
        float attackTimeElapsed = enemyController.timeElapsedfromAttack();
        return attackTimeElapsed > cooldown;
    }
}