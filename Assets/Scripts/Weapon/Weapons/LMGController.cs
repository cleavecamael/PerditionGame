using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class LMGController : WeaponController
{
    protected LMG lmgStats;
    protected Vector3 noise;
    private float startVolume;
    [SerializeField] private UnityEvent<float> setShootDrag;
    [SerializeField] private UnityEvent resetShootDrag;
    AudioSource gunSource;
    Coroutine audioCoroutine;
    void Awake()
    {

        lmgStats = weapon as LMG;
        gunSource = AudioManager.getChannel("Gun");
        startVolume = gunSource.volume;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (lmgStats.customShootDrag > 0)
            setShootDrag.Invoke(lmgStats.customShootDrag);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (lmgStats.customShootDrag > 0)
            resetShootDrag.Invoke();
    }
    protected override void OnShootStart()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
            audioCoroutine = null;
        }
        gunSource.pitch = 1;
        gunSource.volume = startVolume;
        AudioManager.playContinuous(gunSource, "LMGSynth");
        StartCoroutine(increasePitch(3f));
    }

    protected override void OnShootEnd()
    {

        gunSource = AudioManager.getChannel("Gun");
        //audioCoroutine = StartCoroutine(FadeAudioSource.StartFade(gunSource, 0.1f, startVolume, 0f));
        // Debug.Log("Stop");
        AudioManager.stopSource(gunSource);
        gunSource.pitch = 1;


    }
    IEnumerator increasePitch(float duration)
    {
        gunSource = AudioManager.getChannel("Gun");
        float val = 0f;
        gunSource.pitch = 1;
        for (int i = 0; i < 100; i++)
        {
            if (gunSource.pitch < 1.3)//1.1224554916f)
            {
                gunSource.pitch = Mathf.Lerp(1f, 1.3f, i / 100f);
            }

            yield return new WaitForSeconds(duration / 100);
        }
    }
    protected override void Shoot()
    {
        if (lmgStats.customShootDrag > 0)
            setShootDrag.Invoke(lmgStats.customShootDrag);
        FireCameraShake();
        canShoot = false;
        currentAmmo--;
        GameObject b;
        base.shootMuzzleFlash.Invoke(true);
        Vector3 startingPos = playerPosition.pos + shootDirection.pos * weapon.gunSpriteOffset;
        b = bulletPooler.GetBullet(startingPos);
        if (lmgStats.spreadAndDamageUpgrade)
        {
            if (!moving())
            {
                noise = new Vector3(0, 0, 0);
                ;
            }
            else
            {
                noise = AddNoiseOnAngle(-lmgStats.currentSpread, lmgStats.currentSpread);
            }
        }
        else
        {
            noise = AddNoiseOnAngle(-lmgStats.currentSpread, lmgStats.currentSpread);
        }
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.Fire(shootDirection.pos + noise, weapon.currentBulletSpeed);
    }
    protected bool moving()
    {
        if ((playerPosition.pos.x - prevPosition.x == 0) && (playerPosition.pos.y - prevPosition.y == 0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Vector3 AddNoiseOnAngle(float min, float max)
    {
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);
        float zNoise = 0;
        Vector3 noise = new Vector3(
            Mathf.Sin(2 * Mathf.PI * xNoise / 360),
            Mathf.Sin(2 * Mathf.PI * yNoise / 360),
            Mathf.Sin(2 * Mathf.PI * zNoise / 360)
         );
        return noise;
    }

    protected override void FireCameraShake()
    {
        shootCameraShake.Invoke(lmgStats.shakeParameters);
        // Debug.Log(lmgStats.shakeParameters);
    }
}