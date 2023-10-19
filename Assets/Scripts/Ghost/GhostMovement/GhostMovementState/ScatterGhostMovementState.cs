using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGhostMovementState : TPGhostMovementState
{
    public override void BeforeChange()
    {
        context.ReverseDirection();
    }

    protected override Vector2 GetTargetPoint()
    {
        return context.GetFixedTargetPoint();
    }
}