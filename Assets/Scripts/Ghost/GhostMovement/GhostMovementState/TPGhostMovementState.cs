using UnityEngine;

public abstract class TPGhostMovementState : GhostMovementState
{
    public override int GetTurningDirectionIndex(Vector2 interPos, int currentDirInd)
    {
        Vector2 targetPoint;

        targetPoint = GetTargetPoint();
        return ChooseDirection(targetPoint, interPos, currentDirInd);
    }

    protected abstract Vector2 GetTargetPoint();

    private int ChooseDirection(Vector2 targetPoint, Vector2 interPos, int currentDirInd)
    {
        float[] dist = new float[4];
        int newDirectionIndex;
        int oppositeDirIndex;
        float minDist;

        oppositeDirIndex = MyDirUtils.GetOppositeDirectionIndex(currentDirInd);
        for (int i = 0; i < 4; i++)
        {
            if (!context.GetIsLegalDir(i) || i == oppositeDirIndex)
            {
                dist[i] = float.MaxValue;
                continue;
            }
            dist[i] = (interPos + MyDirUtils.Int2Dir(i) - targetPoint).sqrMagnitude;
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
        if (dist[newDirectionIndex] == float.MaxValue) return oppositeDirIndex;
        return newDirectionIndex;
    }
}