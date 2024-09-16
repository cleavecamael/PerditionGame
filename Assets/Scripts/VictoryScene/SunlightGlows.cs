using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Collections;

public class SunlightGlows : MonoBehaviour
{

    private Light2D light2D;
    private Light2D globalLight;
    private float startIntensity = 0f; // The initial intensity
    private float currentIntensity;
    private float endIntensity = 1f; // The final intensity
    private float duration = 45f; // The duration of the change in seconds
    private float time = 0;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
        StartCoroutine(IncreaseLightIntensity());

    }
    IEnumerator IncreaseLightIntensity()
    {
        while (time < duration)
        {
            if (time > 5f)
            {
                currentIntensity = startIntensity + (endIntensity - startIntensity) * (time - 5) / duration;
                globalLight.intensity = currentIntensity;
            }
            time += Time.deltaTime;
            yield return null;
        }
        globalLight.intensity = endIntensity; // Ensure the light intensity is exactly the end value at the end of the duration
    }
    void Update()
    {
        Flicker();
    }

    void Flicker()
    {
        float noise = Mathf.PerlinNoise(Time.time, 0.0f);
        light2D.intensity = Mathf.Lerp(currentIntensity, currentIntensity * 2, noise);
        if (time < 5)
            light2D.intensity = Mathf.Lerp(currentIntensity, currentIntensity, noise);
    }
}
