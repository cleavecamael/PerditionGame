using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public abstract class WeaponController : MonoBehaviour
{
    public UnityEvent<Vector2> shootCameraShake;
    public UnityEvent<bool> shootMuzzleFlash;
    [SerializeField] protected Weapon weapon;
    [SerializeField] protected AudioSource weaponSource;
    public Weapon Weapon
    {
        get
        {
            return weapon;
        }
    }
    [SerializeField] protected Vector3Position playerPosition;
    [SerializeField] protected Vector3Position shootDirection;
    [SerializeField] protected UnityEvent<bool> toggleShootDrag;
    [SerializeField] protected UnityEvent<bool> reloadEvent;
    [SerializeField] protected ReloadProgress reloadProgress;
    [SerializeField] private Transform spriteTransform;
    protected bool canShoot;
    protected float freqTimer;
    protected BulletPooler bulletPooler;
    protected bool toShoot;
    protected float currentAmmo;
    protected bool reloading;
    protected Vector3 prevPosition;
    protected float flashTimeSeconds = 0.1f;
    protected float flashTimer;


    public float CurrentAmmo
    {
        get
        {
            return currentAmmo;
        }
    }
    protected bool MagFull
    {
        get
        {
            return currentAmmo == weapon.currentAmmoCapacity;
        }
    }
    private Coroutine reloadCoroutine;
    public WeaponType weaponType;

    protected virtual void Start()
    {
        currentAmmo = weapon.currentAmmoCapacity;
        bulletPooler = GetComponentInChildren<BulletPooler>();
    }

    protected virtual void OnEnable()
    {
        ResetGun();
    }

    protected virtual void OnDisable()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            reloading = false;
        }
    }

    void FixedUpdate()
    {
        if (!reloading)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer > flashTimeSeconds)
            {
                shootMuzzleFlash.Invoke(false);
                flashTimer = 0;
            }
            if (currentAmmo <= 0)
            {
                Reload();
            }
            else if (!canShoot)
            {
                freqTimer += Time.deltaTime;
                if (freqTimer > weapon.currentFrequency)
                {
                    freqTimer = 0;
                    canShoot = true;
                }
            }
            else if (toShoot && canShoot)
            {
                FireAudio();
                Shoot();
            }
        }
        prevPosition = playerPosition.pos;
    }

    void ResetGun()
    {
        toShoot = false;
        freqTimer = weapon.currentFrequency;
        spriteTransform.localEulerAngles = Vector3.zero;
    }

    public virtual void OnShootTrigger(bool activ)
    {
        if (toShoot && !activ)
        {
            OnShootEnd();
        }
        if (!toShoot && activ)
        {
            if (!reloading)
            {
                OnShootStart();
            }
           
        }
        toShoot = activ;
        if (!reloading) SetShootDrag(activ);
    }

    protected virtual void OnShootStart()
    {

    }
    protected virtual void OnShootEnd()
    {

    }
    public virtual void Reload()
    {
        if (!reloading && !MagFull)
        {
            shootMuzzleFlash.Invoke(false);
            // Debug.Log("MuzzleFlashFalse");
            OnShootEnd();
            AudioManager.playClip(weaponSource, "GunReload");
            reloadCoroutine = StartCoroutine(ReloadAnimation());
        }
    }

    IEnumerator ReloadAnimation()
    {
        reloading = true;
        reloadProgress.currentProgress = 0f;
        reloadEvent.Invoke(reloading);
        float timeElapsed = 0;
        SetShootDrag(false);

        while (timeElapsed < weapon.currentReloadTime)
        {
            float rotationThisFrame = weapon.reloadSpinSpeed * Time.deltaTime;
            spriteTransform.Rotate(new Vector3(0, 0, rotationThisFrame));
            timeElapsed += Time.deltaTime;
            reloadProgress.currentProgress = timeElapsed / weapon.currentReloadTime;
            yield return null;
        }
        if (Mouse.current.leftButton.isPressed) SetShootDrag(true);

        spriteTransform.localEulerAngles = Vector3.zero;
        currentAmmo = weapon.currentAmmoCapacity;
        reloading = false;
        reloadEvent.Invoke(reloading);
        freqTimer = 0;
        canShoot = true;
        AudioManager.playClip(weaponSource, "GunLoaded");
        if (toShoot)
        {
            OnShootStart();
        }
    }

    void SetShootDrag(bool t)
    {
        toggleShootDrag.Invoke(t);
    }

    protected virtual void Shoot()
    {
        FireCameraShake();
        shootMuzzleFlash.Invoke(true);
        // Debug.Log("MuzzleFlashTrue  ");
        canShoot = false;
        currentAmmo--;

        Vector3 startingPos = playerPosition.pos + shootDirection.pos * weapon.gunSpriteOffset;

        GameObject b = bulletPooler.GetBullet(startingPos);
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.Fire(shootDirection.pos, weapon.currentBulletSpeed);
    }
    protected virtual void FireAudio()
    {
        AudioManager.playClip(GetComponent<AudioSource>(), "PistolFire");
    }

    protected abstract void FireCameraShake();
}
