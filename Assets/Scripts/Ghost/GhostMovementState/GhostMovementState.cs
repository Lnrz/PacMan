using UnityEngine;

public abstract class GhostMovementState
{
    protected GhostMovement context;

    public void SetContext(GhostMovement context)
    {
        this.context = context;
    }

    public abstract int GetTurningDirectionIndex(Vector2 interPos);

    public abstract void BeforeChange();
}