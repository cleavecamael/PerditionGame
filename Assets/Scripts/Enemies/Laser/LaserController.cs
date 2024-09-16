using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public bool laserState = false;
    bool damagingPlayer = false;
    private float dmgFreqTimer = 0;
    private bool canDamage;
    [SerializeField] private Transform source;
    [SerializeField] private HeadAdjuster rotator; 
    [SerializeField] private float dmgFrequency;
    [SerializeField] private float damage;
    [SerializeField] private Vector3Position playerPosition;
    [SerializeField] private UnityEvent<float> damagePlayer;
    Vector3 startTransform;
    void Start()
    {
        startTransform = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (laserState)
        {
            CheckDamagePlayer();
        }
        transform.localPosition = startTransform;
        
    }

    public void adjustHead()
    {
        rotator.adjustHead();
    }
    public bool clockwiseToPlayer()
    {
        return Vector3.Cross((Vector2)playerPosition.pos - (Vector2)source.position, (Vector2)this.transform.position - (Vector2)source.position).z >= 1;
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
               damagingPlayer = true;
        }
    }
    IEnumerator rotateNonStop()
    {
        activateLaser();
        while (true)
        {
            rotate(1);
            yield return new WaitForSeconds(0.0001f);
        }
    }
    public void activateLaser()
    {
        laserState = true;
        GetComponent<Animator>().SetTrigger("Start");
    }
    public void deactivateLaser()
    {
        laserState = false;
        GetComponent<Animator>().SetTrigger("End");
    }

    public void rotate(float angle)
    {
        source.transform.Rotate(new Vector3(0, 0, angle));
        //transform.RotateAround(source.position, transform.forward, angle);
    }
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            damagingPlayer = false;
        }

    }

    void CheckDamagePlayer()
    {
        if (damagingPlayer)
        {
            if (!canDamage)
            {
                dmgFreqTimer -= Time.deltaTime;
                if (dmgFreqTimer < 0)
                {
                    canDamage = true;
                    dmgFreqTimer = dmgFrequency;
                }
            }
            else
            {
                canDamage = false;
                damagePlayer.Invoke(damage);
            }
        }
    }
}
