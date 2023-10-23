using UnityEngine;

public class AkabeiMovement : GhostMovement
{
    public override Vector2 GetTargetPoint()
    {
        return pacman.position;
    }
}