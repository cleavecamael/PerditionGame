using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Bat/BatMeleeEndAction")]
public class BatMeleeEndAction : Action
{
    public override void Act(StateController controller)
    {
        controller.GetComponent<BatController>().MeleeEnd();
     

    }
}
