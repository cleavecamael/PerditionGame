using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponCollectionManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Weapon weapon;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponDescription;
    public TextMeshProUGUI weaponStats;
    public GameObject screenOverlay;
    private Image thisImage;
    private Button thisButton;


    public GameObject upgrade11;
    public GameObject upgrade12;
    public GameObject upgrade13;
    public GameObject upgrade21;
    public GameObject upgrade22;
    public GameObject upgrade23;
    public Image weaponIcon;
    public TextMeshProUGUI screenWeaponName;

    void Start()
    {
        thisImage = GetComponent<Image>();
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(LoadScreenOverlay);
        weaponName.text = weapon.name;
        thisImage.sprite = weapon.Icon;
        thisImage.color = new Color(255, 255, 255);
        screenOverlay.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.3f, 0.3f, 0);
    }

    void LoadScreenOverlay()
    {
        screenOverlay.SetActive(true);
        upgrade11.GetComponent<Image>().sprite = weapon.upgradePath.upgrade11.Icon;
        upgrade12.GetComponent<Image>().sprite = weapon.upgradePath.upgrade12.Icon;
        upgrade13.GetComponent<Image>().sprite = weapon.upgradePath.upgrade13.Icon;
        upgrade21.GetComponent<Image>().sprite = weapon.upgradePath.upgrade21.Icon;
        upgrade22.GetComponent<Image>().sprite = weapon.upgradePath.upgrade22.Icon;
        upgrade23.GetComponent<Image>().sprite = weapon.upgradePath.upgrade23.Icon;

        upgrade11.GetComponent<WeaponCollectionButton>().SetWeaponUpgrade(weapon.upgradePath.upgrade11);
        upgrade12.GetComponent<WeaponCollectionButton>().SetWeaponUpgrade(weapon.upgradePath.upgrade12);
        upgrade13.GetComponent<WeaponCollectionButton>().SetWeaponUpgrade(weapon.upgradePath.upgrade13);
        upgrade21.GetComponent<WeaponCollectionButton>().SetWeaponUpgrade(weapon.upgradePath.upgrade21);
        upgrade22.GetComponent<WeaponCollectionButton>().SetWeaponUpgrade(weapon.upgradePath.upgrade22);
        upgrade23.GetComponent<WeaponCollectionButton>().SetWeaponUpgrade(weapon.upgradePath.upgrade23);
        weaponIcon.sprite = weapon.Icon;
        screenWeaponName.text = weapon.name;
        weaponDescription.text = weapon.Description;
        weaponStats.text = "Base stats:\n" + weapon.ShowStats();
    }
}
