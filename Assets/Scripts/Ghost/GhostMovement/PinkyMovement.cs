using UnityEngine;

public class PinkyMovement : GhostMovement
{
    private AbstractMovingEntity pacmanME;
    
    public override Vector2 GetTargetPoint()
    {
        Vector2 targetPoint;

        if (pacmanME is null)
        {
            pacmanME = pacman.GetComponent<AbstractMovingEntity>();
        }
        targetPoint = pacman.position;
        targetPoint += 4 * Utility.Int2Dir(pacmanME.GetDirectionIndex());
        return targetPoint;
    }
}