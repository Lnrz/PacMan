using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyMovement : GhostMovement
{
    private AbstractMovingEntity pacmanME;
    
    private void Awake()
    {
        pacmanME = pacman.GetComponent<AbstractMovingEntity>();
    }

    public override Vector2 GetTargetPoint()
    {
        Vector2 targetPoint;

        targetPoint = pacman.position;
        targetPoint += 4 * Utility.Int2Dir(pacmanME.GetDirectionIndex());
        return targetPoint;
    }
}