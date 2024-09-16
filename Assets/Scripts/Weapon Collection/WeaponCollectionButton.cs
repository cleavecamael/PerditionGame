using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponCollectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI weaponUpgrade;
    private WeaponUpgrade weaponUpgradeSO;
    public void SetWeaponUpgrade(WeaponUpgrade weaponUpgrade)
    {
        weaponUpgradeSO = weaponUpgrade;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0);
        weaponUpgrade.text = weaponUpgradeSO.name + ":\n" + weaponUpgradeSO.Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.3f, 0.3f, 0);
        weaponUpgrade.text = "";
    }

}
