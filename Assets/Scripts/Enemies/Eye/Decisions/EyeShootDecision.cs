using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/EyeShootDecision")]
public class EyeshootDecision : Decision
{
    public Collider2DSO col;
    public Vector3Position pos;
    public float distance;
    public override bool Decide(StateController controller)
    {
        Collider2D collider = col.col;

        Vector3 nearestPoint = collider.ClosestPoint(controller.transform.position);

        return ((controller.transform.position - nearestPoint).magnitude) < distance 
            && controller.GetComponent<EyeController>().hitPlayer();
    }
}