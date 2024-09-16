using UnityEngine;
using UnityEngine.Events;

public class PistolController : WeaponController
{
    public Pistol pistolStats;
    [SerializeField] private UnityEvent<float> setShootDrag;
    [SerializeField] private UnityEvent resetShootDrag;

    void Awake()
    {
        pistolStats = weapon as Pistol;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (pistolStats.customShootDrag > 0)
            setShootDrag.Invoke(pistolStats.customShootDrag);
    }
    public override void OnShootTrigger(bool activ)
    {
        base.OnShootTrigger(activ);
        if (pistolStats.customShootDrag > 0)
            setShootDrag.Invoke(pistolStats.customShootDrag);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (pistolStats.customShootDrag > 0)
            resetShootDrag.Invoke();
    }
    protected override void FireCameraShake()
    {
        shootCameraShake.Invoke(pistolStats.shakeParameters);
    }
}