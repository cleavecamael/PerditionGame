using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static EnemyProjectileStats;
using Random = UnityEngine.Random;

public class AttackPattern : MonoBehaviour
{
    public GameObject AttackPrefab;
    public float totalDuration;
    List<(Vector2,float)>bulletIntervals = new List<(Vector2, float)>();
    public GameObject bullet;
    //up for no rotation
    public Vector2 rotationVec = Vector2.up;
    public float rotationInterval = 0;
    public float rotateValue = 0;
    [SerializeField] private string fireSound;
    [SerializeField] private bool shootAll;
    [SerializeField] private Vector3Position playerPosition;
    [SerializeField] private bool evenInterval = true;
    [SerializeField] private bool homing;
    [SerializeField] private bool orientOnce = false;
    [SerializeField] private bool orientCycle = false;
    [SerializeField] private bool debug = false;
    [SerializeField] private bool fixedRotate = false;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float spread;
  

    // Start is called before the first frame update
    void Start()
    {
        
        setPattern();
        if (debug)
        {
            activatePattern();
        } 
        if (rotationInterval > 0)
        {
            StartCoroutine(rotate());
        }
    
    }

    public void activatePattern(float time)
    {
        StartCoroutine(FireAndStop(time));
    }

   public void activatePattern()
    {
        StartCoroutine(FireNonStop());
    }
    void rotateToPlayer()
    {
        rotationVec = playerPosition.pos - this.transform.position;
    }
    IEnumerator rotate()
    {
       
        while (true)
        {
            rotationVec = Quaternion.Euler(0, 0, rotateValue) * rotationVec;
            yield return new WaitForSeconds(rotationInterval);
        }
        
    }
    void setPattern()
    {
        //Sort children by distance, furthest away goes first
        int childCount = AttackPrefab.transform.childCount;
     
        Transform[] childrenList = new Transform[childCount];
        float[] distances = new float[childCount];
        int count = 0;
        foreach (Transform child in AttackPrefab.transform)
        {
            childrenList[count] = child;
            distances[count] = child.localPosition.magnitude;
            count++;
        }
        Array.Sort(distances, childrenList);
        Array.Reverse(childrenList);



        //compute individual timestamps
     
        float scale = childrenList[0].localPosition.magnitude;
        float currentTimestamp = 0;
        float fixedInterval = totalDuration / (float)childCount;

        for (int i = 0; i < childCount; i++)
        {
  
            float timestamp = totalDuration - ((childrenList[i].localPosition.magnitude / scale) * totalDuration);
            float nextTimestamp;
            if (i+ 1 != childCount)
            {
                 nextTimestamp = totalDuration - ((childrenList[i + 1].localPosition.magnitude / scale) * totalDuration);
            }
            else
            {
                 nextTimestamp = totalDuration;
            }
            float currInterval;
            if (evenInterval)
            {
                currInterval = fixedInterval;
            }
            else if (shootAll)
            {
                currInterval = (i == childCount - 1) ? totalDuration : 0;
            }
            else
            {
                currInterval = nextTimestamp - timestamp;
            }
           
             bulletIntervals.Add((childrenList[i].localPosition.normalized, currInterval));
            currentTimestamp = timestamp;

        }
    }
    IEnumerator FireAndStop(float seconds)
    {
        Coroutine fireCoroutine = StartCoroutine(FireNonStop());
        yield return new WaitForSeconds(seconds);
        StopCoroutine(fireCoroutine);
    }

    IEnumerator FireOnce()
    {
        
        foreach ((Vector2, float) bulletInterval in bulletIntervals) {
            Vector2 vectorDir = bulletInterval.Item1;
            float interval = bulletInterval.Item2;
            GameObject tmp = Instantiate(bullet, this.transform.position, Quaternion.identity);
            EnemyBullet b = GetComponent<EnemyBullet>();
            b.Initialize(lifetime, damage);
            Vector2 adjustedVec = adjustedDirection(vectorDir);
            if (spread != 0)
            {
                adjustedVec = addSpread(adjustedVec, spread);
            }
            
            if (!fireSound.Equals(""))
            {
                AudioManager.playClip(fireSound);
            }
            
            b.Fire(adjustedVec, speed);
            for (float duration = interval; duration > 0; duration -= Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
           
        }


    }

    Vector2 addSpread(Vector2 vector, float spread)
    {
        float halfSpread = spread / 2;
        float degreeDelta = Random.Range(-halfSpread, halfSpread);
        return Quaternion.Euler(0, 0, degreeDelta) * vector;
    }


    IEnumerator FireNonStop()
    {
        if (orientOnce)
        {
            rotateToPlayer();
        }
        while (true)
        {
            if (fixedRotate)
            {
                rotationVec = Quaternion.Euler(0, 0, rotateValue) * rotationVec;
            }
            if (orientCycle)
            {
                rotateToPlayer();
            }
            foreach ((Vector2, float) bulletInterval in bulletIntervals)
            {
                Vector2 vectorDir = bulletInterval.Item1;
                float interval = bulletInterval.Item2;
                GameObject tmp = Instantiate(bullet, this.transform.position, Quaternion.identity);
                if (homing)
                {
                    rotateToPlayer();
                }
                EnemyBullet b = tmp.GetComponent<EnemyBullet>();
                b.Initialize(lifetime, damage);
                Vector2 adjustedVec = adjustedDirection(vectorDir);
                if (spread != 0)
                {
                    adjustedVec = addSpread(adjustedVec, spread);
                }
           
                if (!fireSound.Equals(""))
                {
                    AudioManager.playClip(fireSound);
                }
                b.Fire(adjustedVec, speed);
                if (interval == 0)
                {
                    continue;
                }
                for (float duration = interval; duration > 0; duration -= Time.fixedDeltaTime)
                {
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
    IEnumerator FireStop()
    {
        StopCoroutine(FireNonStop());
        yield return null;
    }

    Vector2 adjustedDirection(Vector2 direction)
    {
        return Quaternion.FromToRotation(Vector2.up, rotationVec) * direction;
    }

    void Fire()
    {

    }
}
