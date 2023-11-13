using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RepeatingProjectileShooter : ProjectileShooter
{
    [SerializeField, Header("Rate of fire"), FormerlySerializedAs("rateOfFire")]
    protected float baseRateOfFire = 1f;
    [SerializeField]
    protected float minRateOfFire = 1f;
    [SerializeField]
    protected float rateOfFireRedPerShot = 0.05f;
    [SerializeField]
    protected float timingOffset;
    protected float currentRateOfFire;

    protected float timeSinceLastFire;
    protected bool firing;
    
    protected virtual void Start()
    {
        firing = false;
        currentRateOfFire = baseRateOfFire;
        Invoke(nameof(EnableFire), timingOffset);
    }

    protected void EnableFire()
    {
        firing = true;
    }

    void Update()
    {
        if (!firing) return;

        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= currentRateOfFire)
        {
            ShootProjectile();
        }
    }

    protected override void ShootProjectile()
    {
        timeSinceLastFire = 0;
        ReduceRateOfFire();
        base.ShootProjectile();
    }

    protected void ReduceRateOfFire()
    {
        currentRateOfFire -= rateOfFireRedPerShot;
        currentRateOfFire = Mathf.Clamp(currentRateOfFire, minRateOfFire, baseRateOfFire);
    }
}
