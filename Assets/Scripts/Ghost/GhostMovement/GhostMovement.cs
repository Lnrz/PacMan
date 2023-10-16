using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public abstract class GhostMovement : AbstractMovingEntity, GhostStateManagerObserver
{
    [SerializeField] protected Transform pacman;
    [SerializeField] private Vector2 fixedTargetPoint;
    private GhostMovementState state;
    private bool isWaitingToReverseDir;
    private int reverseDirIndex;

    protected override sealed void UpdateHelper()
    {
        if (isWaitingToReverseDir && IsCloseToTileCenter())
        {
            ChangeDirection(reverseDirIndex);
            isWaitingToReverseDir = false;
            reverseDirIndex = -1;
        }
    }

    public override sealed void IntersectionStopEnter(Vector3 interPos)
    {
        int newDirectionIndex;

        newDirectionIndex = state.GetTurningDirectionIndex(interPos);
        if (newDirectionIndex == (GetDirectionIndex() + 2) % 4)
        {
            ReverseDirection();
        }
        else
        {            
            ChangeDirection(newDirectionIndex);
        }
    }

    public abstract Vector2 GetTargetPoint();

    public Vector2 GetFixedTargetPoint()
    {
        return fixedTargetPoint;
    }

    public void ReverseDirection()
    {
        isWaitingToReverseDir = true;
        reverseDirIndex = (GetDirectionIndex() + 2) % 4;
    }

    private bool IsCloseToTileCenter()
    {
        Vector2 dist;

        dist.x = Mathf.Abs(transform.position.x - Mathf.Floor(transform.position.x) - 0.5f);
        dist.y = Mathf.Abs(transform.position.y - Mathf.Floor(transform.position.y) - 0.5f);

        return Mathf.Max(dist.x, dist.y) < 0.001f;
    }

    public void UpdateState(GhostStateAbstractFactory factory)
    {
        if (!enabled) enabled = true;
        state?.BeforeChange();
        state = factory.GetMovementState();
        state.SetContext(this);
    }
}