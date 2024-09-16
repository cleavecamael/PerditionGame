using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Missile : MonoBehaviour
{

    public UltiConstants ultiConstants;
    private Vector2 targetPosition;
    public Vector2 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }
    public UnityEvent<Vector2> startShootingEvent;
    public bool startShootingEventCalled = false;
    public GameObject explosionObject;
    private Animator missileAnimator;
    private Vector2 shakeParameters = new Vector2(0.4f, 5f); // amount, duration

    void Awake()
    {
        Vector3 rotationAxis = new Vector3(0, 0, 1);
        float rotationAmount = -90f;
        transform.Rotate(rotationAxis, rotationAmount);
        missileAnimator = GetComponent<Animator>();
    }

    IEnumerator SpawnExplosionCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            AudioManager.playClip(GetComponent<AudioSource>(), "Explosion");
            SpawnExplosion();
            yield return new WaitForSeconds(.5f);
        }
        Deactivate();
    }

    void SpawnExplosion()
    {
        Vector2 randomPos2D = UnityEngine.Random.insideUnitCircle * ultiConstants.radius;

        Vector3 randomPos3D = new Vector3(randomPos2D.x, randomPos2D.y, 0);

        randomPos3D += transform.position;

        Instantiate(explosionObject, randomPos3D, Quaternion.identity);
    }
    void Update()
    {
        if (transform.position.y > targetPosition.y)
        {
            SetVelocity(ultiConstants.velocityMissile);
        }
        else
        {
            missileAnimator.SetTrigger("Shoot");
            SetVelocity(0);
            if (!startShootingEventCalled)
            {
                startShootingEventCalled = true;
                startShootingEvent.Invoke(shakeParameters);
                StartCoroutine(SpawnExplosionCoroutine());
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacles")
        {
            gameObject.SetActive(false);
        }
    }



    public void Deactivate()
    {
        Destroy(gameObject);
    }

    public void SetVelocity(float velocity)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * velocity;
    }


}
