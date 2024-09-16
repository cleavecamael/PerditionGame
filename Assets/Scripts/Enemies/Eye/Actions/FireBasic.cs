using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/FireBasic")]
public class FireBasic : Action
{
    public override void Act(StateController controller)
    {
        controller.gameObject.GetComponent<EyeController>().FireBasic();

    }
}
