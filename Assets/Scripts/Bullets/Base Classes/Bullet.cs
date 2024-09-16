using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Damage { get; set; }
    public float CurrentLifetime { get; set; }
    public GameConstants gameConstants;
    public FeverMeterScore feverMeterScore;

    public float DealDamage()
    {
        if (feverMeterScore.activeFever)
        {
            return Damage * feverMeterScore.damageBuff;
        }
        else
        {
            return Damage;
        }
    }

    protected virtual void Update()
    {
        CurrentLifetime -= Time.deltaTime;
        if (CurrentLifetime < 0) gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "Interactive")
        {
            gameObject.SetActive(false);
        }
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public virtual void Fire(Vector3 direction, float speed)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

}
