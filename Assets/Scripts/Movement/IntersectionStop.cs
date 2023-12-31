using UnityEngine;

public class IntersectionStop : MonoBehaviour
{
    private static readonly float sqrdMaxExitDist = 1.0f;
    [SerializeField] private bool[] turnDirection = new bool[4];
    private int directionIndex;
    private Vector2 dist;
    private GameObject go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        go = collision.gameObject;
        if (go.TryGetComponent<AbstractMovingEntity>(out AbstractMovingEntity movingEnt))
        {
            movingEnt.SetLegalDir(turnDirection);
            movingEnt.IntersectionStopEnter(transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        go = collision.gameObject;
        if (go.TryGetComponent<AbstractMovingEntity>(out AbstractMovingEntity movingEnt))
        {
            CheckForCollision(movingEnt);
        }
    }
    
    private void CheckForCollision(AbstractMovingEntity movingEnt)
    {
        directionIndex = movingEnt.GetDirectionIndex();
        if (directionIndex == -1 || turnDirection[directionIndex]) return;
        dist = movingEnt.transform.position - transform.position;
        if (HasCollided())
        {
            movingEnt.Stop(true, false);
            movingEnt.LockDirection(directionIndex, true);
        }
    }

    private bool HasCollided()
    {
        return
            (directionIndex == 0 && dist.y >= 0) ||
            (directionIndex == 1 && dist.x >= 0) ||
            (directionIndex == 2 && dist.y <= 0) ||
            (directionIndex == 3 && dist.x <= 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        go = collision.gameObject;
        if (go.TryGetComponent<AbstractMovingEntity>(out AbstractMovingEntity movingEnt) &&  IsNotTooDistantFromIntersection(go))
        {
            movingEnt.LockDirection((directionIndex + 1) % 4, true);
            movingEnt.LockDirection((directionIndex + 3) % 4, true);
        }
    }

    private bool IsNotTooDistantFromIntersection(GameObject other)
    {
        dist = other.transform.position - transform.position;
        return dist.sqrMagnitude <= sqrdMaxExitDist;
    }

    public bool CanTurn(int directionIndex)
    {
        return turnDirection[directionIndex];
    }
}