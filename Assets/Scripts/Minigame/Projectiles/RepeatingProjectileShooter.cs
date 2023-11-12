using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingProjectileShooter : ProjectileShooter
{
    [SerializeField]
    protected float rateOfFire = 1f;
    [SerializeField]
    protected float timingOffset;

    protected float timeSinceLastFire;
    protected bool firing;
    
    protected virtual void Start()
    {
        firing = false;
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

        if (timeSinceLastFire >= rateOfFire)
        {
            ShootProjectile();
        }
    }

    protected override void ShootProjectile()
    {
        timeSinceLastFire = 0;
        base.ShootProjectile();
    }
}
