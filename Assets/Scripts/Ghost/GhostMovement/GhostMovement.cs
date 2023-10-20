using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;

public abstract class GhostMovement : AbstractMovingEntity
{
    [SerializeField] protected Transform pacman;
    [SerializeField] private Vector2 fixedTargetPoint;
    [SerializeField] private GhostHouseSettings settings;
    private GhostMovementState state = new StillGhostMovementState();
    private bool isWaitingToReverseDir = false;
    private int reverseDirIndex = -1;

    protected void Awake()
    {
        state.SetContext(this);
        for (int i = 0; i < 4; i++)
        {
            LockDirection(i, !settings.IsTurnableDir(i));
        }
        if (TryGetComponent<ChangeStateEventInvoker>(out ChangeStateEventInvoker changeStateEventInvoker))
        {
            changeStateEventInvoker.OnChangeState(UpdateState);
        }
        AwakeHelper();
    }

    protected abstract void AwakeHelper();

    public Vector2 GetGhostHouseExitPosition()
    {
        return settings.GetExitPosition();
    }

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

    public void TakeRandomInitialDirection()
    {
        int directionIndex;

        directionIndex = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            if (GetIsLegalDir(directionIndex)) break;
            directionIndex = (directionIndex + 1) % 4;
        }
        ChangeDirection(directionIndex);
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

    private void UpdateState(GhostStateAbstractFactory factory)
    {
        state.BeforeChange();
        state = factory.GetMovementState();
        state.SetContext(this);
    }
}