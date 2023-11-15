using UnityEngine;

public static class MyDirUtils
{
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

    public static int Dir2Int(Vector2 dir)
    {
        int res = -1;

        if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            res = (dir.x >= 0) ? 1 : 3;
        }
        else
        {
            res = (dir.y >= 0) ? 0 : 2;
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
}