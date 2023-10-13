using UnityEngine;

public abstract class TPGhostState : GhostState
{
    public sealed override int GetTurningDirectionIndex(Vector2 interPos)
    {
        Vector2 targetPoint;

        targetPoint = GetTargetPoint();
        return ChooseDirection(targetPoint, interPos);
    }

    protected abstract Vector2 GetTargetPoint();

    private int ChooseDirection(Vector2 targetPoint, Vector2 interPos)
    {
        float[] dist = new float[4];
        int newDirectionIndex;
        float minDist;

        for (int i = 0; i < 4; i++)
        {
            if (!context.GetIsLegalDir(i) || i == (context.GetDirectionIndex() + 2) % 4)
            {
                dist[i] = float.MaxValue;
                continue;
            }
            dist[i] = (interPos + Utility.Int2Dir(i) - targetPoint).sqrMagnitude;
        }
        newDirectionIndex = 0;
        minDist = dist[0];
        for (int i = 1; i < 4; i++)
        {
            if (minDist > dist[i])
            {
                minDist = dist[i];
                newDirectionIndex = i;
            }
        }
        if (dist[newDirectionIndex] == float.MaxValue) return (context.GetDirectionIndex() + 2) %  4;
        return newDirectionIndex;
    }

    public override void BeforeChange()
    {
        context.ReverseDirection();
    }
}