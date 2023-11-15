using UnityEngine;

public abstract class GhostMovement : AbstractMovingEntity
{
    private static readonly float goHomeSpeedMod = 1.5f;
    [SerializeField] protected Transform pacman;
    [SerializeField] private Vector2 fixedTargetPoint;
    [SerializeField] private GhostHouseSettings settings;
    [SerializeField] private SpeedSettingsChannelSO speedSettingsChannel;
    private float normalSpeedMod;
    private float frightenedSpeedMod;
    private float tunnelSpeedMod;
    private float currentSpeedMod;
    private GhostMovementState state;
    private bool isWaitingToReverseDir = false;

    protected override sealed void AwakeHelper()
    {
        ResetState();
        ResetLegalDir();
        speedSettingsChannel.AddListener(OnSpeedSettingsChange);
        if (TryGetComponent<ChangeStateEventInvoker>(out ChangeStateEventInvoker changeStateEventInvoker))
        {
            changeStateEventInvoker.OnChangeState(UpdateState);
        }
        if (TryGetComponent<InsideHomeEventInvoker>(out InsideHomeEventInvoker insideHomeEventInvoker))
        {
            insideHomeEventInvoker.OnInsideHome(ResetLegalDir);
        }
    }

    private void OnSpeedSettingsChange(SpeedSettingsSO speedSettings)
    {
        normalSpeedMod = speedSettings.GetGhostNormalSpeedMod();
        frightenedSpeedMod = speedSettings.GetGhostFrightenedSpeedMod();
        tunnelSpeedMod = speedSettings.GetGhostTunnelSpeedMod();
        ChangeToNormalSpeedMod();
    }

    protected override void OnGameRestart()
    {
        ResetState();
        CancelReverseDirection();
        ChangeToNormalSpeedMod();
        ResetLegalDir();
    }

    public Vector2 GetGhostHouseExitPosition()
    {
        return settings.GetExitPosition();
    }

    protected override sealed void UpdateHelper()
    {
        if (isWaitingToReverseDir && Utility.IsCloseToTileCenter(transform.position, sqrdTurnDist))
        {
            ChangeDirection(Utility.GetOppositeDirectionIndex(GetDirectionIndex()));
            CancelReverseDirection();
        }
    }

    protected override sealed void IntersectionStopEnterHelper(Vector3 interPos)
    {
        int newDirectionIndex;

        newDirectionIndex = state.GetTurningDirectionIndex(interPos, GetDirectionIndex());
        if (newDirectionIndex == Utility.GetOppositeDirectionIndex(GetDirectionIndex()))
        {
            ReverseDirection();
        }
        else
        {
            ChangeDirection(newDirectionIndex);
        }
    }

    protected override void StopHelper(int previousDirInd)
    {
        int newDirectionIndex;

        newDirectionIndex = state.GetTurningDirectionIndex(transform.position, previousDirInd);
        ChangeDirection(newDirectionIndex);
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
            directionIndex = Utility.GetNextDirectionIndex(directionIndex);
        }
        ChangeDirection(directionIndex);
    }

    public void ReverseDirection()
    {
        isWaitingToReverseDir = true;
    }

    public void CancelReverseDirection()
    {
        isWaitingToReverseDir = false;
    }

    private void ResetLegalDir()
    {
        SetLegalDir(settings.GetTurnableDirsOutside());
    }

    private void ResetState()
    {
        state = new StillGhostMovementState();
        state.SetContext(this);
    }

    private void UpdateState(GhostStateAbstractFactory factory)
    {
        state.BeforeChange();
        state = factory.GetMovementState();
        state.SetContext(this);
        state.AfterChange();
    }

    public void ChangeToNormalSpeedMod()
    {
        ChangeSpeedMod(normalSpeedMod);
        currentSpeedMod = normalSpeedMod;
    }

    public void ChangeToFrightenedSpeedMod()
    {
        ChangeSpeedMod(frightenedSpeedMod);
        currentSpeedMod = frightenedSpeedMod;
    }

    public void ChangeToGoHomeSpeedMod()
    {
        ChangeSpeedMod(goHomeSpeedMod);
        currentSpeedMod = goHomeSpeedMod;
    }

    public void ChangeToTunnelSpeedMod()
    {
        ChangeSpeedMod(tunnelSpeedMod);
    }

    public void RestoreSpeedMod()
    {
        ChangeSpeedMod(currentSpeedMod);
    }
}