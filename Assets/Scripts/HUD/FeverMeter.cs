using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FeverMeter : MonoBehaviour
{
    [SerializeField] private GameConstants gameConstants;
    [SerializeField] private FeverMeterScore feverMeterScore;
    private Image bar;
    public Image bloodoverlay;
    private float currentFeverBarVal;
    private float currentFeverMultiplier;
    public float targetFever;
    private TextMeshProUGUI multiplier;
    private bool FeverMode = false;
    private TextMeshProUGUI keyPress;
    private AudioSource audioSource;
    private bool flickerState = false;

    // for pulse
    private float maxAlpha = 0.03f; // Maximum alpha value
    private float minAlpha = 0f;
    private float colorAlpha = 0f;

    void Awake()
    {
        bar = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        keyPress = GameObject.Find("FeverKeyPress").GetComponent<TextMeshProUGUI>();
        multiplier = GameObject.Find("DmgMultiplier").GetComponent<TextMeshProUGUI>();
        bloodoverlay = GameObject.Find("RampageImage").GetComponent<Image>();
        currentFeverBarVal = 0f;
        bloodoverlay.color = Color.clear;
        feverMeterScore.activeFever = false;
    }

    void MoveFever()
    {
        targetFever = Mathf.Max(0, targetFever);
        currentFeverBarVal = Mathf.MoveTowards(currentFeverBarVal, targetFever, gameConstants.feverFillSpeed * Time.deltaTime);
        bar.fillAmount = currentFeverBarVal / gameConstants.maxFever;
    }
    void Update()
    {
        MoveFever();
        if (!FeverMode && currentFeverBarVal < gameConstants.maxFever)
        {
            currentFeverMultiplier = 1f;
            feverMeterScore.CurrentFever = 0f;
            HideOverlay();
        }
        else if (FeverMode && currentFeverBarVal > 0)
        {
            targetFever -= gameConstants.feverDecay * Time.deltaTime;
            Pulse();
        }
        else if (FeverMode && currentFeverBarVal <= 0)
        {
            currentFeverMultiplier = 1f;
            FeverMode = false;
            feverMeterScore.activeFever = false;
            HideOverlay();
        }
        multiplier.text = "x" + currentFeverMultiplier.ToString();
        feverMeterScore.CurrentFever = currentFeverBarVal;

        if (!FeverMode && currentFeverBarVal / gameConstants.maxFever > 0.1)
        {
            keyPress.enabled = true;
        }
        else
        {
            keyPress.enabled = false;
        }
    }

    public void AddFever(float amt)
    {
        if (!FeverMode)
        {
            targetFever += amt;
            targetFever = Mathf.Min(gameConstants.maxFever, targetFever);
            // Debug.Log("currentFever" + targetFever);
        }
    }

    public void UseFever()
    {
        if (keyPress.enabled)
        {
            currentFeverMultiplier = feverMeterScore.damageBuff;
            targetFever = 0;
            FeverMode = true;
            feverMeterScore.activeFever = true;
            AudioManager.playClip(audioSource, "zap-rampage");
        }
    }

    void Pulse()
    {
        float noise = Mathf.PerlinNoise(Time.time, 0.0f);
        colorAlpha = Mathf.Lerp(minAlpha, maxAlpha, noise);
        Color color = Color.red;
        color.a = colorAlpha;
        bloodoverlay.color = color;
    }


    void HideOverlay()
    {
        bloodoverlay.color = Color.clear;
    }

}