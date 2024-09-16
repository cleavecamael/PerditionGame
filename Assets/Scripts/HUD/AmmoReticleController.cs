using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoReticleController : MonoBehaviour
{
    private Transform weaponTransform;
    private TextMeshPro text;


    void Awake()
    {
        weaponTransform = GameObject.Find("Weapons").transform;
        text = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        GameObject weapon = GetGameObject();
        if (weapon != null)
        {
            text.text = "" + weapon.GetComponent<WeaponController>().CurrentAmmo;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = Camera.main.nearClipPlane;
        transform.position = mousePos + new Vector3(0.5f, 0.5f, 0);


    }

    GameObject GetGameObject()
    {
        foreach (Transform child in weaponTransform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}