using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Pebble/PebbleMeleeAction")]
public class PebbleMeleeAction : Action
{
    public override void Act(StateController controller)
    {
        Debug.Log("Attacking.");

    }
}
