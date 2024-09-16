using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/StopMove")]
public class StopMove : Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<BaseEnemyController>().StopMove();
        controller.gameObject.GetComponent<Animator>().SetBool("Move", false);

    }
}
