using UnityEngine;

public class StillGhostMovementState : GhostMovementState
{
    public override void BeforeChange()
    {
        context.TakeRandomInitialDirection();
    }

    public override void AfterChange()
    {
        context.CancelReverseDirection();
        context.Stop(false, true);
    }

    public override int GetTurningDirectionIndex(Vector2 interPos, int currentDirInd)
    {
        return -1;
    }
}