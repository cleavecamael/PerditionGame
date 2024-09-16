using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyBullet : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> damagePlayer;
    private float currentLifetime;
    private float damage;

    public float CurrentLifetime { get => currentLifetime; set => currentLifetime = value; }
    public float Damage { get => damage; set => damage = value; }

    protected virtual void Update() 
    {
        CurrentLifetime -= Time.deltaTime;
        if (CurrentLifetime < 0) destroyBullet();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Obstacles")
        {
            gameObject.SetActive(false);
            if (collision.gameObject.tag == "Player")
            {
                damagePlayer.Invoke(Damage);
            }
        }

    }

    public void Initialize(float lifetime, float dmg)
    {
        CurrentLifetime = lifetime;
        Damage = dmg;
    }


    public void Fire(Vector3 direction, float speed)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    protected virtual void destroyBullet()
    {
        Destroy(this.gameObject);
    }
}