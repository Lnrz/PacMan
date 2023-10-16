using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGhostMovementState : TPGhostMovementState
{
    protected override Vector2 GetTargetPoint()
    {
        return context.GetTargetPoint();
    }
}