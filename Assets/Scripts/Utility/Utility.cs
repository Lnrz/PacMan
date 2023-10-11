using UnityEngine;

public static class Utility
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
}