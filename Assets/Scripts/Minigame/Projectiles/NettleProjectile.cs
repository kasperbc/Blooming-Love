using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NettleProjectile : Projectile
{
    [SerializeField]
    private float fallSpeed;

    [SerializeField]
    private AnimationCurve sidewaysMovementCurve;
    [SerializeField]
    private float sidewaysMovementIntensity = 1;
    [SerializeField]
    private float sidewaysMovementSpeed = 1;
    [SerializeField]
    private float sideMoveRotationIntensity = 20f;
    private bool reverseSideMovement;

    private float movementTime;

    private Transform spriteObj;

    protected override void Start()
    {
        reverseSideMovement = Random.value > 0.5f;

        spriteObj = transform.GetChild(0);

        base.Start();
    }

    protected override void Move()
    {
        float reverseMovementMultiplier = 1;
        if (reverseSideMovement) reverseMovementMultiplier = -1;

        rb.velocity = new(
            sidewaysMovementCurve.Evaluate(movementTime * sidewaysMovementSpeed) * sidewaysMovementIntensity * reverseMovementMultiplier, 
            -fallSpeed);

        spriteObj.rotation = Quaternion.Euler(0, 0, 
            sidewaysMovementCurve.Evaluate(movementTime * sidewaysMovementSpeed) * sideMoveRotationIntensity * -reverseMovementMultiplier);

        movementTime += Time.deltaTime;
    }
}
