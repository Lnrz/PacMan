using UnityEngine;

public class ChaseGhostMovementState : TPGhostMovementState
{
    public override void BeforeChange()
    {
        context.ReverseDirection();
    }

    public override void AfterChange() {}

    protected override Vector2 GetTargetPoint()
    {
        return context.GetTargetPoint();
    }
}