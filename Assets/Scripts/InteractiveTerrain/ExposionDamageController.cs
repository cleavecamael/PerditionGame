using UnityEngine;

public class ExplosionDamageController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (collider.gameObject.TryGetComponent<BaseEnemyController>(out BaseEnemyController controller))
            {
                controller.TakeDamage(80f);
            }
        }
    }
}