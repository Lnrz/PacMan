using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillGhostMovementState : GhostMovementState
{
    public override void BeforeChange()
    {
        context.TakeRandomInitialDirection();
    }

    public override void AfterChange()
    {
        context.Stop();
    }

    public override int GetTurningDirectionIndex(Vector2 interPos)
    {
        return -1;
    }
}