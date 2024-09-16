using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/SetTrigger")]
public class SetAnimatorTriggerAction : Action
{
    public string triggerName;
    public override void Act(StateController controller)
    {

        controller.gameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }
}
