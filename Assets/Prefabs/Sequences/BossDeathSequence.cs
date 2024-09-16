using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//todo make this script more modular
public class BossDeathSequence : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string bossId;
    [SerializeField] private bool animated;
    [SerializeField] UnityEvent clearLevel;
    GameObject player;
     GameObject boss;
    private Camera bossCamera;
    

    void Start()
    {
        player = Camera.main.gameObject;
        boss = GameObject.Find(bossId);
        bossCamera = boss.GetComponent<Camera>();
        this.transform.position = player.transform.position;
        
    }
    public void startSequence()
    {
        StartCoroutine(bossDies());
    }

    IEnumerator bossDies()
    {
        yield return new WaitForSeconds(0.2f);
        //disable boss from moving
        boss.GetComponent<StateController>().enabled = false;
        //move to Boss
        Vector3 temp = player.transform.position;
        temp.z = -1;
        transform.position = temp;
        Time.timeScale = 0.01f;
        bossCamera = this.GetComponent<Camera>();
        yield return StartCoroutine(moveToBoss());
        yield return StartCoroutine(death());
        yield return StartCoroutine(moveToPlayer());
        //add a fade somewhere
        Time.timeScale = 1;
        
        yield return new WaitForSecondsRealtime(0.5f);
        clearLevel.Invoke();
    }
    IEnumerator moveToBoss()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 interpolatedVec = Vector2.Lerp(player.transform.position, boss.transform.position, i * 0.02f);
            interpolatedVec.z = -1;
            transform.position = interpolatedVec;
            yield return new WaitForSecondsRealtime(0.002f);
        }
        
    }
    IEnumerator moveToPlayer()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 interpolatedVec = Vector2.Lerp(boss.transform.position, player.transform.position, i * 0.02f);
            interpolatedVec.z = -1;
            transform.position = interpolatedVec;
            yield return new WaitForSecondsRealtime(0.001f);
        }

    }
    IEnumerator zoom(float start, float end)
    {
        float interval = (end - start) / 50;
        float currentZoom = start;
      
        for (int i = 0; i < 50; i++)
        {

            bossCamera.orthographicSize = bossCamera.orthographicSize + interval;
            yield return new WaitForSecondsRealtime(0.001f);
        }
       
    }
    IEnumerator death()
    {
        if (animated)
        {
            boss.GetComponent<Animator>().SetTrigger("Death");
        }
       
        AudioManager.playClip("golemBossRoar");
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
       
    }

    IEnumerator shake()
    {
        Vector3 originalPosition = transform.position;
        for (int i = 0;  i < 20; i++)
        {
            float x = originalPosition.x + Random.Range(-0.5f, 0.5f) * 1f;
            float y = originalPosition.y + Random.Range(-0.5f, 0.5f) * 1f;
            transform.position = new Vector3(x, y, originalPosition.z);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        transform.position = originalPosition;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
      
          
    }
}
