using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonyProjectile : MoveFromBoundaryProjectile
{
    [SerializeField]
    protected float timeUntilGrow;
    private float timeInExistance;

    [SerializeField]
    protected AnimationCurve growCurve;

    protected override void Update()
    {
        base.Update();

        transform.localScale = Vector3.one * growCurve.Evaluate(timeInExistance - timeUntilGrow);

        timeInExistance += Time.deltaTime;
    }
}
