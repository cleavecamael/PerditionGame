using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBlink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void startBlink()
    {
        StartCoroutine(delayActivate());
    }
    
    void endBlink()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    IEnumerator delayActivate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
