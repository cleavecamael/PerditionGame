using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/SpawnGolemDecision")]
public class SpawnGolemDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        GolemBossController enemyController = controller.GetComponent<GolemBossController>();
        return (!enemyController.spawnedGolems && (enemyController.currentHealth < 50));
    }
}