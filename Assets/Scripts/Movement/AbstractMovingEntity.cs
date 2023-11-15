using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractMovingEntity : MonoBehaviour
{
    private static readonly float speed = 6.5f;
    protected static readonly float sqrdTurnDist = 0.0324f;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private StopEntitiesChannelSO stopEntitiesChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    private UnityEvent<int> directionListeners = new UnityEvent<int>();
    private bool[] legalDir = new bool[4];
    private float speedMod = 1.0f;
    private int directionIndex = -1;
    private int nextDirectionIndex = -1;
    private Vector3 direction = Vector3.zero;
    private Vector3 lastInterPos = Vector3.zero;
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

    protected abstract void IntersectionStopEnterHelper(Vector3 interPos);

    public void IntersectionStopEnter(Vector3 interPos)
    {
        lastInterPos = interPos;
        IntersectionStopEnterHelper(interPos);
    }

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

    public void Stop(bool adjustPos, bool forceStop)
    {
        int previousDirInd;

        direction = Vector3.zero;
        previousDirInd = directionIndex;
        UpdateDirectionIndex(-1);
        if (adjustPos)
        {
            MyGridUtils.AdjustPosition(transform);
        }
        if (!forceStop)
        {
            StopHelper(previousDirInd);
        }
    }

    protected abstract void StopHelper(int previousDirInd);

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
            direction = MyDirUtils.Int2Dir(directionIndex);
            legalDir[MyDirUtils.GetOppositeDirectionIndex(directionIndex)] = true;
            MyGridUtils.AdjustPositionToAxis(transform, MyDirUtils.GetAxisIndex(directionIndex));
            nextDirectionIndex = -1;
        }
    }

    private bool IsNextDirectionTurnable()
    {
        if (!IsDirectionValid(nextDirectionIndex)) return false;
        if (MyDirUtils.GetAxisIndex(nextDirectionIndex) == MyDirUtils.GetAxisIndex(directionIndex)) return true;
        return directionIndex == -1 || (transform.position - lastInterPos).sqrMagnitude <= sqrdTurnDist;
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
        Stop(false, true);
        nextDirectionIndex = -1;
        isAcceptingInput = false;
    }

    private void UpdateDirectionIndex(int newDirectionIndex)
    {
        directionIndex = newDirectionIndex;
        directionListeners.Invoke(directionIndex);
    }
}