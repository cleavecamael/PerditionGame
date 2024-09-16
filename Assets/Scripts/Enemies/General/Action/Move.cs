using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/Move")]
public class Move : Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<BaseEnemyController>().CheckMove();
        controller.gameObject.GetComponent<Animator>().SetBool("Move", true);
    }
}
