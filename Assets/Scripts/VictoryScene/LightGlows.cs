using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Collections;

public class LightGlows : MonoBehaviour
{

    public Light2D light2D;
    public Light2D globalLight;
    private float startIntensity = 0f; // The initial intensity
    private float currentIntensity;
    private float endIntensity = 1f; // The final intensity
    public float duration = 5f; // The duration of the change in seconds
    private float time = 0;
    public float timeStartGlow = 5;

    void Start()
    {
        StartCoroutine(IncreaseLightIntensity());
    }
    IEnumerator IncreaseLightIntensity()
    {
        while (time < duration)
        {
            if (time > timeStartGlow)
            {
                currentIntensity = startIntensity + (endIntensity - startIntensity) * (time - timeStartGlow) / duration;
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
        light2D.intensity = Mathf.Lerp(currentIntensity, currentIntensity * 10, noise);
        if (time < timeStartGlow)
            light2D.intensity = Mathf.Lerp(currentIntensity, currentIntensity, noise);
    }
}
