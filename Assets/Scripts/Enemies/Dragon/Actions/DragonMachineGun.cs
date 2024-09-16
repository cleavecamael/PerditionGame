using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/Dragon/DragonMachineGun")]
public class DragonMachineGun : Action
{

    public override void Act(StateController controller)
    {
        controller.GetComponent<DragonController>().machineGunAttack();
    }
}
