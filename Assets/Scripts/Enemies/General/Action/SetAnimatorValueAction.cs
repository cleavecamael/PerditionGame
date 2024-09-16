using UnityEngine;



[CreateAssetMenu(menuName = "PluggableSM/Actions/SetAnimatorBool")]
public class SetAnimatorBool : Action
{
    public string boolName;
    public bool value;
    public override void Act(StateController controller)
    {
     
        controller.gameObject.GetComponent<Animator>().SetBool(boolName, value);
       
    }
}
