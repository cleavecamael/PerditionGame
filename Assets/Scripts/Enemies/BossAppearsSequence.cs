using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


//todo make this script more modular
public class BossAppearsSequence : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string bossId;
    [SerializeField] private bool animated;
    [SerializeField] UnityEvent playBossBGM;
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
        StartCoroutine(bossAppears());
    }

    IEnumerator bossAppears()
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
        playBossBGM.Invoke();
        yield return new WaitForSecondsRealtime(0.3f);
        
        yield return StartCoroutine(zoom(10, 4));
        yield return StartCoroutine(roar());
        yield return StartCoroutine(zoom(4, 10));
        yield return StartCoroutine(moveToPlayer());
        Time.timeScale = 1;
        boss.gameObject.GetComponent<StateController>().enabled = true;
        this.gameObject.SetActive(false);
    }

    IEnumerator playBGM()
    {
        yield return new WaitForSecondsRealtime(2f);
        playBossBGM.Invoke();
    }
    IEnumerator moveToBoss()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 interpolatedVec = Vector2.Lerp(player.transform.position, boss.transform.position, i * 0.02f);
            interpolatedVec.z = -1;
            transform.position = interpolatedVec;
            yield return new WaitForSecondsRealtime(0.02f);
        }

    }
    IEnumerator moveToPlayer()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 interpolatedVec = Vector2.Lerp(boss.transform.position, player.transform.position, i * 0.02f);
            interpolatedVec.z = -1;
            transform.position = interpolatedVec;
            yield return new WaitForSecondsRealtime(0.03f);
        }

    }
    IEnumerator zoom(float start, float end)
    {
        float interval = (end - start) / 50;
        float currentZoom = start;

        for (int i = 0; i < 50; i++)
        {

            bossCamera.orthographicSize = bossCamera.orthographicSize + interval;
            yield return new WaitForSecondsRealtime(0.003f);

        }
    }
        IEnumerator roar()
        {
            if (animated)
            {
                boss.GetComponent<Animator>().SetTrigger("Roar");
            }

            AudioManager.playClip("golemBossRoar");
            Time.timeScale = 0f;
            yield return StartCoroutine(shake());

        }

        IEnumerator shake()
        {
            Vector3 originalPosition = transform.position;
            for (int i = 0; i < 20; i++)
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

