using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromBoundaryProjectile : Projectile
{
    [HideInInspector]
    public Vector2 dir;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float rotationSpeed = 90;
    protected override void Move()
    {
        rb.velocity = dir * moveSpeed;

        // YEAHHHHH!!! LETS GOOOO!!!!!!!
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        base.Move();
    }
}
