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
        context.Stop(false);
    }

    public override int GetTurningDirectionIndex(Vector2 interPos)
    {
        return -1;
    }
}