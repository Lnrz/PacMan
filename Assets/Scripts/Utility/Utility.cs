using UnityEngine;

public static class Utility
{
    private static readonly float gridOffset = 0.5f;

    public static Vector2 Int2Dir(int directionIndex)
    {
        Vector3 res = Vector3.zero;

        switch (directionIndex)
        {
            case 0:
                res = Vector3.up;
                break;
            case 1:
                res = Vector3.right;
                break;
            case 2:
                res = Vector3.down;
                break;
            case 3:
                res = Vector3.left;
                break;
        }
        return res;
    }

    public static int GetOppositeDirectionIndex(int directionIndex)
    {
        if (directionIndex < 0 || directionIndex > 3) return -1;
        return (directionIndex + 2) % 4;
    }

    public static int GetNextDirectionIndex(int directionIndex)
    {
        if (directionIndex < 0 || directionIndex > 3) return -1;
        return (directionIndex + 1) % 4;
    }

    public static int GetAxisIndex(int directionIndex)
    {
        if (directionIndex < 0 || directionIndex > 3) return -1;
        return directionIndex % 2;
    }

    public static bool IsCloseToTileCenter(Vector2 pos, float maxDist)
    {
        float sqrdDist;
        float sqrdMaxDist;

        sqrdDist = GetDistFromTileCenter(pos).sqrMagnitude;
        sqrdMaxDist = maxDist * maxDist;
        return sqrdDist <= sqrdMaxDist;
    }

    public static bool IsCloseToTileCenterAlongAxis(Vector2 pos, int axisIndex,float maxDist)
    {
        float dist;

        dist = GetDistFromTileCenterAlongAxis(pos, axisIndex);
        dist = Mathf.Abs(dist);
        return dist <= maxDist;
    }

    public static void AdjustPosition(Transform transf)
    {
        Vector2 pos;

        pos = transf.position;
        pos = Vec2Floor(pos);
        pos.x = pos.x + gridOffset;
        pos.y = pos.y + gridOffset;
        transf.position = pos;
    }

    public static void AdjustPositionToAxis(Transform transf, int axisIndex)
    {
        Vector2 pos;

        pos = transf.position;
        switch (axisIndex)
        {
            case 0:
                pos.x = Mathf.Floor(pos.x);
                pos.x = pos.x + gridOffset;
                break;
            case 1:
                pos.y = Mathf.Floor(pos.y);
                pos.y = pos.y + gridOffset;
                break;
        }
        transf.position = pos;
    }

    private static Vector2 GetDistFromTileCenter(Vector2 pos)
    {
        Vector2 res;

        res = pos - Vec2Floor(pos);
        res.x = res.x - gridOffset;
        res.y = res.y - gridOffset;
        return res;
    }

    private static float GetDistFromTileCenterAlongAxis(Vector2 pos, int axisIndex)
    {
        float res = gridOffset;

        switch (axisIndex)
        {
            case 0:
                res = pos.x - Mathf.Floor(pos.x);
                break;
            case 1:
                res = pos.y - Mathf.Floor(pos.y);
                break;
        }
        res = res - gridOffset;
        return res;
    }

    private static Vector2 Vec2Floor(Vector2 vec)
    {
        vec.x = Mathf.Floor(vec.x);
        vec.y = Mathf.Floor(vec.y);
        return vec;
    }
}