using UnityEngine;

public class AosukeMovement : GhostMovement
{
    [SerializeField] private Transform akabei;
    private AbstractMovingEntity pacmanME;

    public override Vector2 GetTargetPoint()
    {
        Vector2 targetPoint;

        if (pacmanME is null)
        {
            pacmanME = pacman.GetComponent<AbstractMovingEntity>();
        }
        targetPoint = pacman.position; 
        targetPoint += 2 * MyDirUtils.Int2Dir(pacmanME.GetDirectionIndex());
        targetPoint += (targetPoint - (Vector2)akabei.position);
        return targetPoint;
    }
}