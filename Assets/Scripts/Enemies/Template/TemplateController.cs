using UnityEngine;

public class TemplateController : BaseEnemyController
{
    //can be object pool or prefab
    public GameObject BasicBullet;
    public override void Die()
    {
        base.Die();
    }
}
