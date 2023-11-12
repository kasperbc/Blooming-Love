using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerProjectileShooter : RepeatingProjectileShooter
{
    protected override Vector2 GetProjectileSpawnPosition()
    {
        return (Vector2)transform.position + Vector2.up;
    }
}
