using UnityEngine;
using UnityEngine.UI;

public class ExitWeaponUpgradePath : MonoBehaviour
{
    private Button thisButton;
    public GameObject screenOverlay;
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(ExitScreenOverlay);
    }
    void ExitScreenOverlay()
    {
        screenOverlay.SetActive(false);
    }
}
