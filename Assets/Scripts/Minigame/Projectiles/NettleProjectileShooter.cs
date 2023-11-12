using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NettleProjectileShooter : RepeatingProjectileShooter
{
    [SerializeField]
    protected float spawnRange;

    protected override Vector2 GetProjectileSpawnPosition()
    {
        return transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new(transform.position.x - spawnRange, transform.position.y), new(transform.position.x + spawnRange, transform.position.y));
    }
}
