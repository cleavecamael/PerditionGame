using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Golem/GolemMeleeActionEnd")]
public class GolemMeleeActionEnd : Action
{
    public override void Act(StateController controller)
    {
        controller.GetComponent<GolemController>().MeleeEnd();
     

    }
}
