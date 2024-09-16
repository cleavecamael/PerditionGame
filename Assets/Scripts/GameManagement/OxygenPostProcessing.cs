using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OxygenPostProcessing : MonoBehaviour
{
    private bool oxygenRunOut = false;
    private bool coroutineStarted = false;
    public OutOfOxygenConstants outOfOxygenConstants;
    public CanvasGroup canvasGroup;
    private float canvasGroupAlphaTarget;
    public RectTransform canvasRectTransform;

    private Volume volume;
    private ChromaticAberration chromaticAberration;
    private float chromaticAberrationTarget;
    // private Bloom bloom;
    // private float bloomTarget;
    // private PaniniProjection paniniProjection;
    // private float paniniProjectionTarget;
    private FilmGrain filmGrain;
    private LensDistortion lensDistortion;
    private float lensDistortionTarget;
    private float interval = 1f;
    public HealthSystem healthSystem;
    public PlayerStats playerStats;
    private float ratio;

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        // volume.profile.TryGet<Bloom>(out bloom);
        // volume.profile.TryGet<PaniniProjection>(out paniniProjection);
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);

        canvasRectTransform = transform.Find("Blood").GetComponent<RectTransform>();
        canvasGroup = transform.Find("Blood").GetComponent<CanvasGroup>();

        Reset();
    }

    void Reset()
    {
        canvasGroup.alpha = 0f;
        chromaticAberration.intensity.value = 0f;
        // bloom.intensity.value = 0f;
        // paniniProjection.distance.value = 0f;
        // filmGrain.intensity.value = 0f;
        lensDistortion.intensity.value = 0f;
        canvasGroupAlphaTarget = 0f;
    }

    void Update()
    {
        ratio = Mathf.Min((float)(Mathf.Max((float)(0.35 - healthSystem.currentHealth / playerStats.health), 0) / 0.2), 1f);
        // Debug.Log("ratio " + ratio);
        // Debug.Log("healht " + healthSystem.currentHealth);
        if (healthSystem.currentHealth / playerStats.health <= 0.35)
        {
            oxygenRunOut = true;
        }
        else
        {
            oxygenRunOut = false;
        }


        if (oxygenRunOut && !coroutineStarted)
        {
            StartCoroutine(WaitOneFrame());
        }
        if (oxygenRunOut)
        {
            Pulse();
        }
        else
        {
            Reset();
        }
    }


    IEnumerator WaitOneFrame()
    {
        coroutineStarted = true;
        yield return null;
        SetPulseTarget();
        coroutineStarted = false;
    }

    void SetPulseTarget()
    {
        chromaticAberrationTarget = Mathf.PingPong(Time.time / interval, outOfOxygenConstants.chromaticAberrationMax * ratio); // This will create a pulsating effect
                                                                                                                               // bloomTarget = Mathf.PingPong(Time.time / interval, outOfOxygenConstants.bloomMax );
                                                                                                                               // paniniProjectionTarget = Mathf.PingPong(Time.time / interval, outOfOxygenConstants.paniniProjectionMax * ratio);
        lensDistortionTarget = Mathf.PingPong(Time.time / interval, outOfOxygenConstants.lensDistortionMax * ratio);
        canvasGroupAlphaTarget = Mathf.PingPong(Time.time / interval, canvasGroupAlphaTarget);
    }

    void Pulse()
    {
        chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, chromaticAberrationTarget, outOfOxygenConstants.chromaticAberrationRate);
        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, lensDistortionTarget, outOfOxygenConstants.lensDistortionRate);
        // bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, bloomTarget, outOfOxygenConstants.bloomRate);
        // paniniProjection.distance.value = Mathf.Lerp(paniniProjection.distance.value, paniniProjectionTarget, outOfOxygenConstants.paniniProjectionRate);
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, outOfOxygenConstants.maxCanvasAlpha, outOfOxygenConstants.canvasRate * ratio);
    }


}