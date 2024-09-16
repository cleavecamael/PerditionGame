using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class UltiTarget : MonoBehaviour
{
    public UltiConstants ultiConstants;
    public SpriteRenderer areaTarget;
    public Light2D light2D;

    void Start()
    {
        areaTarget = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
        areaTarget.enabled = false;
        light2D.enabled = false;
    }

    public void OnActivate(bool val)
    {
        areaTarget.enabled = val;
        light2D.enabled = val;
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = Camera.main.nearClipPlane;
        transform.position = mousePos;
        if (light2D.enabled)
            Flicker();
    }

    void Flicker()
    {
        float noise = Mathf.PerlinNoise(Time.time, 0.0f);
        light2D.intensity = Mathf.Lerp(0f, 10f, noise);
    }
}