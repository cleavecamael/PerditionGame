using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Golem/GolemMeleeAction")]
public class GolemMeleeAction : Action
{
    public override void Act(StateController controller)
    {
        controller.GetComponent<Animator>().SetTrigger("Attack");
        controller.GetComponent<GolemController>().MeleeStart();
     

    }
}
