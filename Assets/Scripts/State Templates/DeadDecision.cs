using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/IsDead")]
public class DeadDecision : Decision
{
    //Checks if enemy is alive
        
    public override bool Decide(StateController controller)
    {
        return !controller.GetComponent<BaseEnemyController>().alive;
    }
}