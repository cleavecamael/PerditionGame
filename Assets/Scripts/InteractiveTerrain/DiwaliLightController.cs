using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight2D : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        Flicker();
    }

    void Flicker()
    {
        float noise = Mathf.PerlinNoise(Time.time, 0.0f);
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
