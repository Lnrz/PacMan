using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGhostMovementState : TPGhostMovementState
{
    public override void BeforeChange()
    {
        context.ReverseDirection();
    }

    protected override Vector2 GetTargetPoint()
    {
        return context.GetTargetPoint();
    }
}