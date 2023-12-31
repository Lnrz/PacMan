using UnityEngine;

public class GuzutaMovement : GhostMovement
{
    public override Vector2 GetTargetPoint()
    {
        Vector2 pacmanPos;
        Vector2 myPos;
        float dist;

        pacmanPos = pacman.position;
        myPos = transform.position;
        dist = (pacmanPos - myPos).sqrMagnitude;
        if (dist > 64) return pacmanPos;
        return GetFixedTargetPoint();
    }
}