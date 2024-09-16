using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/GolemBoss/SpawnGolemsAction")]
public class SpawnGolemsAction : Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<GolemBossAttacks>().spawnGolem();
    }
}
