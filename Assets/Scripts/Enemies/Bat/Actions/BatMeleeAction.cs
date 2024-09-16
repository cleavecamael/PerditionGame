using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Bat/BatMeleeAction")]
public class BatMeleeAction : Action
{
    public override void Act(StateController controller)
    {
        controller.GetComponent<Animator>().SetTrigger("Attack");
        controller.GetComponent<BatController>().MeleeStart();
     

    }
}
