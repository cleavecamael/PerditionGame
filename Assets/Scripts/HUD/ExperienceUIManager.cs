using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUIManager : MonoBehaviour
{
    [SerializeField] GameConstants gameConstants;
    [SerializeField] XPSystem xpSystem;
    private Image bar;
    private TextMeshProUGUI levelNo;
    private float currentXP;

    void Awake()
    {
        bar = GetComponentInChildren<Image>();
        levelNo = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        currentXP = currentXP < xpSystem.CurrentXP ? Mathf.MoveTowards(currentXP, xpSystem.CurrentXP, gameConstants.xpFillSpeed * Time.deltaTime) : xpSystem.CurrentXP;
        if (xpSystem.ReqXPForCurrentLevel >= 0) bar.fillAmount = (currentXP - xpSystem.ReqXPForPreviousLevel) / (xpSystem.ReqXPForCurrentLevel - xpSystem.ReqXPForPreviousLevel);
        else bar.fillAmount = 1;
        levelNo.text = $"LEVEL: {xpSystem.CurrentLevel.ToString()}";
    }

}