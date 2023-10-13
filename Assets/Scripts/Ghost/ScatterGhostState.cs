using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGhostState : TPGhostState
{
    protected override Vector2 GetTargetPoint()
    {
        return context.GetFixedTargetPoint();
    }
}