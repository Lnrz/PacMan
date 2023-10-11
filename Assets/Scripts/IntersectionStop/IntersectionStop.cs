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
    [SerializeField] private bool[] turnDirection = new bool[4];
    private int directionIndex;
    private Vector2 dist;
    private GameObject go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        go = collision.gameObject;
        if (go.TryGetComponent<AbstractMovingEntity>(out AbstractMovingEntity movingEnt))
        {
            TriggerStay(movingEnt);
            movingEnt.IntersectionStopEnter(transform.position, turnDirection);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        go = collision.gameObject;
        if (go.TryGetComponent<AbstractMovingEntity>(out AbstractMovingEntity movingEnt))
        {
            TriggerStay(movingEnt);
        }
    }

    private void TriggerStay(AbstractMovingEntity movingEnt)
    {
        directionIndex = movingEnt.GetDirectionIndex();
        if (directionIndex == -1) return; // If the entity is not moving then there is no need to check the collision
        dist = movingEnt.transform.position - transform.position;
        
        /* The following piece of code checks if the entity is moving toward a non turnable direction
         * if he is than it also checks if the entity is close enough to the wall
         * if he is than it stops the entity
         */
        if (!turnDirection[directionIndex] && HasCollided())
        {
            movingEnt.Stop();
            movingEnt.AdjustPosition();
            movingEnt.LockDirection(directionIndex, true);
            return; // Since the entity has been stopped there is no need to check if he is going towards the intersection or not
        }
        /* If the entity is going toward the intersection his legal directions will be updated by adding the turnable ones,
         * otherwise if he is going away from the intersection the directions corresponding to the opposite axis,
         * in respect of the one on which he is currently moving, will become illegal.
         */
        if (IsGoingToIntersection())
        {
            UpdateLegalDir(movingEnt);
        }
        else
        {
            movingEnt.LockDirection((directionIndex + 1) % 4, true);
            movingEnt.LockDirection((directionIndex + 3) % 4, true);
        }
    }

    public bool CanTurn(int i)
    {
        return turnDirection[i];
    }

    private void UpdateLegalDir(AbstractMovingEntity movingEnt)
    {
        for (int i = 0; i < 4; i++)
        {
            if (turnDirection[i])
            {
                movingEnt.LockDirection(i, false);
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
