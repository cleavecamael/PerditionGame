public class GolemController : BaseEnemyController
{

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