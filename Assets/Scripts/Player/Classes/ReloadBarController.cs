using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBarController : MonoBehaviour
{
    public ReloadProgress reloadProgress;
    public GameObject parent;

    [SerializeField] protected CanvasGroup canvasGroup;
    int activeWeapon;

    [SerializeField] private Image bar;

    void Awake()
    {
        activeWeapon = 1;
        canvasGroup.alpha = 0f;
        bar.fillAmount = 0;
    }
    public void onReloadEvent(bool val)
    {
        // Debug.Log("reload event " + val);
        if (val)
        {
            canvasGroup.alpha = 1f;
            bar.fillAmount = 0;
        }
        else
            canvasGroup.alpha = 0;
    }
    public void OnSwapWeaponEvent(int val)
    {
        // Debug.Log("swap weapon " + val);
        if (activeWeapon != val)
        {
            canvasGroup.alpha = 0f;
            activeWeapon = val;
        }
    }
    void Update()
    {
        // prevent mirroring
        transform.rotation = Quaternion.identity;
        bar.fillAmount = reloadProgress.currentProgress;
    }
}