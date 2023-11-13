using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryProjectileShooter : RepeatingProjectileShooter
{
    private enum SpawnPos
    {
        Top, Bottom, Right, Left
    }
    private Vector2 spawnDir;

    [SerializeField]
    private Vector2 spawnArea;
    [SerializeField]
    private Vector2 boundaryArea;

    protected override Vector2 GetProjectileSpawnPosition()
    {
        SpawnPos spawnPos = (SpawnPos)Random.Range(0, 3);

        Vector2 pos = Vector2.zero;
        switch (spawnPos)
        {
            case SpawnPos.Top:
                pos = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), boundaryArea.y);
                spawnDir = Vector2.down;
                break;
            case SpawnPos.Bottom:
                pos = new Vector2(Random.Range(-spawnArea.x, spawnArea.x), -boundaryArea.y);
                spawnDir = Vector2.up;
                break;
            case SpawnPos.Right:
                pos = new Vector2(boundaryArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                spawnDir = Vector2.left;
                break;
            case SpawnPos.Left:
                pos = new Vector2(-boundaryArea.x, Random.Range(-spawnArea.y, spawnArea.y));
                spawnDir = Vector2.right;
                break;
        }

        return (Vector2)transform.position + pos;
    }

    protected override void ShootProjectile()
    {
        timeSinceLastFire = 0;
        GameObject p = Instantiate(projectile, GetProjectileSpawnPosition(), Quaternion.identity);
        ReduceRateOfFire();

        if (p.GetComponent<Projectile>() is MoveFromBoundaryProjectile)
        {
            p.GetComponent<MoveFromBoundaryProjectile>().dir = spawnDir;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // genuinely awful code here i hate it so much
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y + boundaryArea.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y - boundaryArea.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x + boundaryArea.x, transform.position.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x - boundaryArea.x, transform.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y + spawnArea.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y - spawnArea.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x + spawnArea.x, transform.position.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x - spawnArea.x, transform.position.y));
    }
}
