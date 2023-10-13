using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGhostState : TPGhostState
{
    protected override Vector2 GetTargetPoint()
    {
        return context.GetTargetPoint();
    }
}