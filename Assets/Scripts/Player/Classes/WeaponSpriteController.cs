using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class WeaponSpriteController : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    private bool reloading;

    public UnityEvent<bool> OnReloadEvent;

    public void StartReload(float reloadTime)
    {
        if (!reloading) StartCoroutine(ReloadAnimation(reloadTime));
    }
    private void SetShootDrag(bool trig)
    {
        GetComponentInParent<PlayerMovement>().ToggleShootingDrag(trig);
    }

    IEnumerator ReloadAnimation(float duration)
    {
        reloading = true;
        OnReloadEvent.Invoke(reloading);
        float timeElapsed = 0;
        SetShootDrag(false);

        while (timeElapsed < duration)
        {
            float rotationThisFrame = spinSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, rotationThisFrame));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (Mouse.current.leftButton.isPressed) SetShootDrag(true);

        transform.localEulerAngles = Vector3.zero;
        reloading = false;
        OnReloadEvent.Invoke(reloading);
    }
}