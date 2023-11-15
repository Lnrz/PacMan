using UnityEngine;

public class FrightenedGhostMovementState : GhostMovementState
{
    public override int GetTurningDirectionIndex(Vector2 interPos, int currentDirInd)
    {
        int newDirectionIndex;
        int oppositeDirIndex;

        oppositeDirIndex = Utility.GetOppositeDirectionIndex(currentDirInd);
        newDirectionIndex = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            if (context.GetIsLegalDir(newDirectionIndex) && newDirectionIndex != oppositeDirIndex)
            {
                return newDirectionIndex;
            }
            newDirectionIndex = Utility.GetNextDirectionIndex(newDirectionIndex);
        }
        return oppositeDirIndex;
    }

    public override void BeforeChange()
    {
        context.ChangeToNormalSpeedMod();
    }

    public override void AfterChange()
    {
        context.ChangeToFrightenedSpeedMod();
    }
}