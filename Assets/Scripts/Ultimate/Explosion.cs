using UnityEngine;

public class Explosion : MonoBehaviour
{
    public UltiConstants ultiConstants;

    public void Deactivate()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (col.gameObject.TryGetComponent<BaseEnemyController>(out BaseEnemyController controller))
            {
                controller.TakeDamage(ultiConstants.damage);
            }
        }
    }

}
