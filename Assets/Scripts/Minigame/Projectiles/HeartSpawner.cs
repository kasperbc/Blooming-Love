using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : RepeatingProjectileShooter
{
    [SerializeField]
    Vector2 spawnArea;
    [SerializeField]
    float minDistanceToPlayer = 2f;
    protected override Vector2 GetProjectileSpawnPosition()
    {
        Vector2 spawnPos = GetRandomSpawnPos();
        Vector2 playerPos = FindObjectOfType<PlayerController>().transform.position;
        while (Vector2.Distance(spawnPos, playerPos) < minDistanceToPlayer)
        {
            spawnPos = GetRandomSpawnPos();
        }

        return spawnPos;
    }

    Vector2 GetRandomSpawnPos()
    {
        return (Vector2)transform.position + new Vector2(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y + spawnArea.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y - spawnArea.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x + spawnArea.x, transform.position.y));
        Gizmos.DrawLine(transform.position, new(transform.position.x - spawnArea.x, transform.position.y));
    }
}
