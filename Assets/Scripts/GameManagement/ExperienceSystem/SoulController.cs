using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoulController : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> xpCollected;
    Rigidbody2D rb;
    Transform target;
    bool hasTarget;
    float pickupTime;
    float backwardForce;
    float impulseDelay;
    bool collected;
    public int XpToGive { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pickupTime = 0.3f;
        backwardForce = 10f;
        impulseDelay = 0.2f;
    }

    void FixedUpdate()
    {
        if (hasTarget)
        {
            rb.velocity = (target.position - transform.position).normalized * (Vector3.Distance(transform.position, target.position) / pickupTime);
            pickupTime -= Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Magnet") && !collected)
        {
            collected = true;
            StartCoroutine(Collect(col.transform));
        }
        else if (col.CompareTag("Player"))
        {
            AudioManager.playClip(col.gameObject.GetComponent<AudioSource>(), "soulPickup");
            xpCollected.Invoke(XpToGive);
            if (transform.parent != null) Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator Collect(Transform t)
    {
        Vector3 dir = (transform.position - t.position).normalized + Vector3.up * 1f;
        float timer = 0;
        while (timer < impulseDelay)
        {
            float backwardSpeed = Mathf.Lerp(backwardForce, 0, timer / impulseDelay);
            transform.position += backwardSpeed * Time.deltaTime * dir;
            timer += Time.deltaTime;
            yield return null;
        }
        hasTarget = true;
        target = t;
    }

    public void SetXP(int xp)
    {
        XpToGive = xp;
    }
}