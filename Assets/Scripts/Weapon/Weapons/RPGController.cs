using UnityEngine;

public class RPGController : WeaponController
{
    [SerializeField]
    protected RPG rpgStats;
    protected override void FireAudio()
    {
        AudioManager.playClip(GetComponent<AudioSource>(), "RPGShot");
    }

    protected override void FireCameraShake()
    {
        shootCameraShake.Invoke(rpgStats.shakeParameters);
    }
}