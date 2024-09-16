using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/Dragon/DragonChooseAttackAction")]
public class DragonChooseAttackAction : Action
{
    public override void Act(StateController controller)
    {
        controller.GetComponent<DragonController>().chooseAttack();
    }
}
