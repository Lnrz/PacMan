using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AosukeMovement : GhostMovement
{
    [SerializeField] private Transform akabei;
    private AbstractMovingEntity pacmanME;

    private void Awake()
    {
        pacmanME = pacman.GetComponent<AbstractMovingEntity>();
    }

    public override Vector2 GetTargetPoint()
    {
        Vector2 targetPoint;

        targetPoint = pacman.position; 
        targetPoint += 2 * Utility.Int2Dir(pacmanME.GetDirectionIndex());
        targetPoint += (targetPoint - (Vector2)akabei.position);
        return targetPoint;
    }
}
