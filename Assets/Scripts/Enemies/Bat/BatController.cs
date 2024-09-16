using UnityEngine.UIElements;
using UnityEngine;

public class BatController : BaseEnemyController
{
    //can be object pool or prefab
    public Hitbox hitbox;
    public override void Die()
    {
        base.Die();
    }

    public void MeleeStart()
    {
        hitbox.dealDamagePlayer();
        hitbox.enableHitbox(true);
        hitbox.rotateToDirection(Aim());

    }
    


    public void MeleeEnd()
    {
        hitbox.enableHitbox(false);
        hitbox.damaged = false;
    }
}
