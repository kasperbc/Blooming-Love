using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerProjectile : Projectile
{
    private Transform player;

    [SerializeField]
    private float moveSpeed = 3f;

    protected override void Start()
    {
        // Rotate towards player
        player = FindObjectOfType<PlayerController>().transform;

        Vector3 targ = player.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        base.Start();
    }

    protected override void Move()
    {
         rb.velocity = transform.right * moveSpeed;
    }
}
