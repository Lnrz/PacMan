using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IntersectionStop : MonoBehaviour
{
    /* Directions have been indexed this way:
     * 
     * 0 : up
     * 1 : right
     * 2 : down
     * 3 : left
     * -1 : not valid
     * (Starting from up going clockwise)
     */
    [SerializeField] private bool[] turnDirection = { false, false, false, false };
    int directionIndex;
    Vector2 dist;

    public void ApplyDuringCollision(PlayerMovement pm)
    {
        directionIndex = pm.getDirectionIndex();
        if (directionIndex == -1) return; // If the player is not moving then there is no need to check the collision
        dist = pm.transform.position - transform.position;
        
        /* The following piece of code checks if the player is moving toward a non turnable direction
         * if he is than it also checks if the player is close enough to the wall
         * if he is than it stops the player
         */
        if (!turnDirection[directionIndex] && HasCollided())
        {
            pm.Stop();
            pm.AdjustPosition();
            pm.LockDirection(directionIndex, true);
            return; // Since the player has been stopped there is no need to check if he is going towards the intersection or not
        }
        /* If the player is going toward the intersection his legal directions will be updated by adding the turnable ones,
         * otherwise if he is going away from the intersection the directions corresponding to the opposite axis,
         * in respect of the one on which he is currently moving, will become illegal.
         */
        if (IsGoingToIntersection())
        {
            UpdateLegalDir(pm);
        }
        else
        {
            pm.LockDirection((directionIndex + 1) % 4, true);
            pm.LockDirection((directionIndex + 3) % 4, true);
        }
    }

    public bool CanTurn(int i)
    {
        return turnDirection[i];
    }

    private void UpdateLegalDir(PlayerMovement pm)
    {
        for (int i = 0; i < 4; i++)
        {
            if (turnDirection[i])
            {
                pm.LockDirection(i, false);
            }
        }
    }

    private bool HasCollided()
    {
        return
            (directionIndex == 0 && dist.y >= 0) ||
            (directionIndex == 1 && dist.x >= 0) ||
            (directionIndex == 2 && dist.y <= 0) ||
            (directionIndex == 3 && dist.x <= 0);
    }

    private bool IsGoingToIntersection()
    {
        return
            (directionIndex < 2 && dist[(directionIndex + 1) % 2] < 0) ||
            (directionIndex > 1 && dist[(directionIndex + 1) % 2] > 0);
    }
}
