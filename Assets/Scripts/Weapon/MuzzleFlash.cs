using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private float intensity;
    private float innerRadius;
    private float outerRadius;

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = transform.Find("Light").GetComponent<Light2D>();
        spriteRenderer.enabled = false;
        light2D.enabled = false;
    }

    void FlashEpilepsy()
    {
        intensity = Random.Range(0.01f, 0.2f);
        outerRadius = Random.Range(1f, 3f);
        innerRadius = Random.Range(0, outerRadius / 3);
    }

    void Update()
    {
        light2D.intensity = Mathf.Lerp(0.01f, intensity, 0.7f);
        light2D.pointLightOuterRadius = Mathf.Lerp(0.5f, outerRadius, 0.7f);
        light2D.pointLightInnerRadius = Mathf.Lerp(0, innerRadius, 0.7f);
    }


    public void OnShoot(bool val)
    {
        spriteRenderer.enabled = val;
        light2D.enabled = val;
        if (!val)
            return;
        var value = Random.value;
        if (value < 0.25)
        {
            spriteRenderer.sprite = sprite0;
            light2D.color = new Color(253f, 223f, 68f);
        }
        else if (value >= 0.25 && value < 0.5)
        {
            spriteRenderer.sprite = sprite1;
            light2D.color = new Color(253f, 223f, 128f);
        }
        else if (value >= 0.5 && value < 0.75)
        {
            spriteRenderer.sprite = sprite2;
            light2D.color = new Color(248f, 170f, 18f);
        }
        else if (value >= 0.75)
        {
            light2D.color = new Color(253f, 173f, 64f);
            spriteRenderer.sprite = sprite3;
        }
        FlashEpilepsy();
    }
}