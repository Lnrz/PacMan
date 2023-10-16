using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightenedGhostMovementState : GhostMovementState
{
    public override int GetTurningDirectionIndex(Vector2 interPos)
    {
        int newDirectionIndex;

        newDirectionIndex = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            if (newDirectionIndex != (context.GetDirectionIndex() + 2 ) % 4 && context.GetIsLegalDir(newDirectionIndex))
            {
                return newDirectionIndex;
            }
            newDirectionIndex = (newDirectionIndex + 1) % 4;
        }
        return (context.GetDirectionIndex() + 2) % 4;
    }

    public override void BeforeChange() {}
}