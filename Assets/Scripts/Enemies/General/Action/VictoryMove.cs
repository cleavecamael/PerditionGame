using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/VictoryMove")]
public class VictoryMove: Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<BaseEnemyController>().victoryMove();
    
    }
}
