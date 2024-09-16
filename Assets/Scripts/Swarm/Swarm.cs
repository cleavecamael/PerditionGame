using UnityEngine;
using UnityEngine.Events;

public class Swarm : MonoBehaviour
{
    public Transform centerPoint;
    private float rotationSpeed = 200f;
    public float damage;
    public XPSystem system;

    [SerializeField] private UnityEvent<Vector3, int> spawnDamageNumbers;

    private void Update()
    {
        // Calculate the rotation axis
        Vector3 axis = new Vector3(0, 0, 1);

        // Rotate the object around the center point
        transform.RotateAround(centerPoint.position, axis, rotationSpeed * Time.deltaTime);

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (collider.gameObject.TryGetComponent<BaseEnemyController>(out BaseEnemyController controller))
            {
                controller.TakeDamage(damage);
            }
        }
    }


}
