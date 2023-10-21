using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeGhostMovementState : TPGhostMovementState
{
    public override void BeforeChange()
    {
        context.Stop();
        context.ChangeToNormalSpeedMod();
    }

    public override void AfterChange()
    {
        context.ChangeToGoHomeSpeedMod();
    }

    protected override Vector2 GetTargetPoint()
    {
        return context.GetGhostHouseExitPosition();
    }
}