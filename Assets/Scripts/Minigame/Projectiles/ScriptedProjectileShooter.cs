using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedProjectileShooter : ProjectileShooter
{
    public float shootTimeAfterEnable;

    protected virtual void Start()
    {
        Invoke(nameof(ShootProjectile), shootTimeAfterEnable);
    }
}
