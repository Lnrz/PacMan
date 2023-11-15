using UnityEngine;

public static class MyGridUtils
{
    private static readonly float gridOffset = 0.5f;

    public static bool IsCloseToTileCenter(Vector2 pos, float maxDist)
    {
        float sqrdDist;
        float sqrdMaxDist;

        sqrdDist = GetDistFromTileCenter(pos).sqrMagnitude;
        sqrdMaxDist = maxDist * maxDist;
        return sqrdDist <= sqrdMaxDist;
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

    private static Vector2 Vec2Floor(Vector2 vec)
    {
        vec.x = Mathf.Floor(vec.x);
        vec.y = Mathf.Floor(vec.y);
        return vec;
    }
}