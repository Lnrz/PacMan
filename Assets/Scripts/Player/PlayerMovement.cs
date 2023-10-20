using UnityEngine;

public class PlayerMovement : AbstractMovingEntity
{
    protected override sealed void UpdateHelper()
    {
        ChangeDirection(GetDirectionInput());
    }

    private int GetDirectionInput()
    {
        int directionIndex = -1;

        if (UpInput())
        {
            directionIndex = 0;
        }
        else if (RightInput())
        {
            directionIndex = 1;
        }
        else if (DownInput())
        {
            directionIndex = 2;
        }
        else if (LeftInput())
        {
            directionIndex = 3;
        }

        return directionIndex;
    }

    private bool UpInput()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool RightInput()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private bool DownInput()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    private bool LeftInput()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public override sealed void IntersectionStopEnter(Vector3 interPos) {}
}