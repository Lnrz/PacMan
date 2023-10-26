using System;
using UnityEngine;

public abstract class AbstractMovingEntity : MonoBehaviour
{
    private static readonly float speed = 4.0f;
    protected static readonly float turnDist = 0.035f;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    private bool[] legalDir = new bool[4];
    private float speedMod = 1.0f;
    private int directionIndex = -1;
    private int nextDirectionIndex = -1;
    private Vector3 direction = Vector3.zero;

    protected void Awake()
    {
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

    protected abstract void OnGameRestartHelper();

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
        directionIndex = -1;
        Utility.AdjustPosition(transform);
    }

    protected void ChangeDirection(int otherDirectionIndex)
    {
        if (IsDirectionValid(otherDirectionIndex))
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
            directionIndex = nextDirectionIndex;
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
        /*
        float dist;

        if (nextDirectionIndex % 2 == 0)
        {
            dist = transform.position.x - Mathf.Floor(transform.position.x);
        }
        else
        {
            dist = transform.position.y - Mathf.Floor(transform.position.y);
        }
        dist = Mathf.Abs(dist - 0.5f);

        return dist <= turnDist;
        */
    }

    private void Move()
    {
        transform.position += speed * speedMod * Time.deltaTime * direction;
    }

    protected void ChangeSpeedMod(float speedMod)
    {
        this.speedMod = speedMod;
    }

    private void OnGameRestart()
    {
        Stop();
        nextDirectionIndex = -1;
        OnGameRestartHelper();
    }
}