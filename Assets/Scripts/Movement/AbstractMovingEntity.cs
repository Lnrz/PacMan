using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractMovingEntity : MonoBehaviour
{
    private static readonly float speed = 5.0f;
    protected static readonly float turnDist = 0.035f;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private StopEntitiesChannelSO stopEntitiesChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    private UnityEvent<int> directionListeners = new UnityEvent<int>();
    private bool[] legalDir = new bool[4];
    private float speedMod = 1.0f;
    private int directionIndex = -1;
    private int nextDirectionIndex = -1;
    private Vector3 direction = Vector3.zero;
    private bool isAcceptingInput = false;

    protected void Awake()
    {
        gameStartChannel.AddListener(OnGameStart);
        stopEntitiesChannel.AddListener(OnStopEntities);
        gameRestartChannel.AddListener(OnGameRestart);
        AwakeHelper();
    }

    protected void Update()
    {
        UpdateHelper();
        ChangeDirection();
        Move();
    }

    protected abstract void AwakeHelper();

    protected abstract void UpdateHelper();

    protected abstract void OnGameRestart();

    public abstract void IntersectionStopEnter(Vector3 interPos);

    public bool GetIsLegalDir(int directionIndex)
    {
        return legalDir[directionIndex];
    }

    public int GetDirectionIndex()
    {
        return directionIndex;
    }

    public void LockDirection(int directionIndex, bool lockMode)
    {
        legalDir[directionIndex] = !lockMode;
    }

    public void SetLegalDir(bool[] newLegalDir)
    {
        Array.Copy(newLegalDir, legalDir, 4);
    }

    public void Stop()
    {
        direction = Vector3.zero;
        UpdateDirectionIndex(-1);
        Utility.AdjustPosition(transform);
    }

    public void AddDirectionListener(UnityAction<int> listener)
    {
        directionListeners.AddListener(listener);
    }

    public void RemoveDirectionListener(UnityAction<int> listener)
    {
        directionListeners.RemoveListener(listener);
    }

    protected void ChangeDirection(int otherDirectionIndex)
    {
        if (isAcceptingInput && IsDirectionValid(otherDirectionIndex))
        {
            nextDirectionIndex = otherDirectionIndex;
            ChangeDirection();
        }
    }

    private bool IsDirectionValid(int otherDirectionIndex)
    {
        return otherDirectionIndex != -1 && legalDir[otherDirectionIndex];
    }

    private void ChangeDirection()
    {
        if (IsNextDirectionTurnable())
        {
            UpdateDirectionIndex(nextDirectionIndex);
            direction = Utility.Int2Dir(directionIndex);
            legalDir[Utility.GetOppositeDirectionIndex(directionIndex)] = true;
            Utility.AdjustPositionToAxis(transform, Utility.GetAxisIndex(directionIndex));
            nextDirectionIndex = -1;
        }
    }

    private bool IsNextDirectionTurnable()
    {
        if (!IsDirectionValid(nextDirectionIndex)) return false;
        if (Utility.GetAxisIndex(nextDirectionIndex) == Utility.GetAxisIndex(directionIndex)) return true;
        return Utility.IsCloseToTileCenterAlongAxis(transform.position, Utility.GetAxisIndex(nextDirectionIndex), turnDist);
    }

    private void Move()
    {
        transform.position += speed * speedMod * Time.deltaTime * direction;
    }

    protected void ChangeSpeedMod(float speedMod)
    {
        this.speedMod = speedMod;
    }

    private void OnGameStart()
    {
        isAcceptingInput = true;
    }

    private void OnStopEntities()
    {
        Stop();
        nextDirectionIndex = -1;
        isAcceptingInput = false;
    }

    private void UpdateDirectionIndex(int newDirectionIndex)
    {
        directionIndex = newDirectionIndex;
        directionListeners.Invoke(directionIndex);
    }
}