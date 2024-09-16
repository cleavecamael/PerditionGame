using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/Orient")]
public class Orient :Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<BaseEnemyController>().orient();
    
    }
}
