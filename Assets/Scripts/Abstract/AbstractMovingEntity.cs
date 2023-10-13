using System;
using UnityEngine;

public abstract class AbstractMovingEntity : MonoBehaviour
{
    [SerializeField] private bool[] legalDir = new bool[4];
    [SerializeField] private float speed = 2;
    private int directionIndex = -1;
    private int nextDirectionIndex = -1;
    private readonly float turnDist = 0.025f;
    private Vector3 direction = Vector3.zero;

    protected void Update()
    {
        UpdateHelper();
        ChangeDirection();
        Move();
    }

    public bool GetIsLegalDir(int directionIndex)
    {
        return legalDir[directionIndex];
    }

    public int GetDirectionIndex()
    {
        return directionIndex;
    }

    public void Stop()
    {
        direction = Vector3.zero;
        directionIndex = -1;
        AdjustPosition();
    }

    public void LockDirection(int directionIndex, bool lockMode)
    {
        legalDir[directionIndex] = !lockMode;
    }

    private void AdjustPosition()
    {
        Vector3 pos;

        pos = transform.position;
        pos.x = MathF.Floor(pos.x) + 0.5f;
        pos.y = MathF.Floor(pos.y) + 0.5f;
        transform.position = pos;
    }

    public abstract void IntersectionStopEnter(Vector3 interPos);

    protected abstract void UpdateHelper();

    protected void ChangeDirection(int otherDirectionIndex)
    {
        if (IsDirectionValid(otherDirectionIndex))
        {
            nextDirectionIndex = otherDirectionIndex;
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (IsNextDirectionTurnable())
        {
            directionIndex = nextDirectionIndex;
            legalDir[(directionIndex + 2) % 4] = true;
            nextDirectionIndex = -1;
            direction = Utility.Int2Dir(directionIndex);
            AdjustPositionToDir();
        }
    }

    private void Move()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private bool IsDirectionValid(int otherDirectionIndex)
    {
        return otherDirectionIndex != -1 && legalDir[otherDirectionIndex];
    }

    private bool IsNextDirectionTurnable()
    {
        if (!IsDirectionValid(nextDirectionIndex)) return false;
        if (nextDirectionIndex % 2 == directionIndex % 2) return true;
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
    }

    private void AdjustPositionToDir()
    {
        Vector3 pos;

        pos = transform.position;
        if (directionIndex % 2 == 0)
        {
            pos.x = Mathf.Floor(transform.position.x) + 0.5f;
        }
        else
        {
            pos.y = Mathf.Floor(transform.position.y) + 0.5f;
        }
        transform.position = pos;
    }
}