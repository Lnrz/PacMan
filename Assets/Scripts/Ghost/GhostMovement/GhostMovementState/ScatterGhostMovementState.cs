using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGhostMovementState : TPGhostMovementState
{
    protected override Vector2 GetTargetPoint()
    {
        return context.GetFixedTargetPoint();
    }
}