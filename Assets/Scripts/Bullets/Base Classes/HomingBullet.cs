using UnityEngine;

public class HomingBullet : EnemyBullet
{
    [SerializeField] private Vector3Position playerPosition;
    private Rigidbody2D rb;
    private float bulletSpeed;
    private float rotateSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float lifetime, float dmg, float speed, float rotSpeed = 200f)
    {
        CurrentLifetime = lifetime;
        Damage = dmg;
        bulletSpeed = speed;
        rotateSpeed = rotSpeed;
    }


    void FixedUpdate()
    {
        Vector2 direction = (Vector2) playerPosition.pos - rb.position;
        direction.Normalize();

        float rotateAmt = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmt * rotateSpeed;

        rb.velocity = transform.up * bulletSpeed; 
    }
    
}