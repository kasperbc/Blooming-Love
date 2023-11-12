using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    protected GameObject projectile;

    protected Vector2 spawnPosition;

    protected virtual void ShootProjectile()
    {
        Instantiate(projectile, GetProjectileSpawnPosition(), Quaternion.identity);
    }

    protected virtual Vector2 GetProjectileSpawnPosition()
    {
        return transform.position;
    }

    protected void DestroySelf()
    {
        Destroy(gameObject);
    }
}
