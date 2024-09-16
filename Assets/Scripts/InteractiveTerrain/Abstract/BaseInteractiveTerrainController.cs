using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class BaseInteractiveTerrainController : MonoBehaviour
{
    public bool TestTrigger = false;
    public bool isActive = true;
    public Collider2D terrainCollider;
    public Animator animator;
    public GameObject powerUp;
    public TerrainConstants terrainConstants;
    public float currentHp;
    protected int maxHp;
    [SerializeField] bool toRespawn;

    void Awake()
    {
        terrainCollider = GetComponent<Collider2D>();
    }

    public void Hit(float dmg)
    {
        currentHp -= dmg;
        animator.SetTrigger("Hit");
    }

    IEnumerator ReSpawn()
    {
        yield return new WaitForSecondsRealtime(terrainConstants.respawn);
        if (!isActive)
        {
            Activate();
        }
    }

    public void Deactivate()
    {
        isActive = false;
        terrainCollider.enabled = false;
        animator.SetBool("Used", true);
        GetComponent<Light2D>().enabled = false;
        if (toRespawn) StartCoroutine(ReSpawn());
    }
    public abstract void SpawnPowerup();
    public void Activate()
    {
        currentHp = maxHp;
        animator.SetBool("Used", false);
        isActive = true;
        terrainCollider.enabled = true;
        GetComponent<Light2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Hit(col.gameObject.GetComponent<Bullet>().Damage);
        }
    }
    void Update()
    {
        if (isActive && TestTrigger)
        {
            Hit(1f);
        }
        if (currentHp <= 0 && isActive)
        {
            Deactivate();
        }
        if (currentHp > 0 && !isActive)
        {
            Activate();
        }

    }


}