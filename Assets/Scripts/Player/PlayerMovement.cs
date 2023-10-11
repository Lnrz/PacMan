using UnityEngine;

public class PlayerMovement : AbstractMovingEntity
{
    /* Directions have been indexed this way:
     * 
     * 0 : up
     * 1 : right
     * 2 : down
     * 3 : left
     * -1 : null
     * (Starting from up going clockwise)
     */
    /* The turning feature works this way:
     * 
     * When the player gives an input direction the code checks if it is legal:
     *     if it is the input direction becomes the nextDirection;
     *     otherwise the input direction is discarded.
     * Then the code checks if the nextDirection is possible:
     *     if it is it becomes the new direction;
     *     otherwise the nextDirection is kept until the player changes it or it becomes possible.
     *
     * Legal
     * means that the direction is not null and that the player can turn to it
     * Possible
     * means that the direction is not null and that the player is close enough to the intersection to turn
     */
    protected override void UpdateHelper()
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

    private bool LeftInput()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    private bool DownInput()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    private bool RightInput()
    {
        return Input.GetKeyDown(KeyCode.D);
    }

    private bool UpInput()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    public override void IntersectionStopEnter(Vector3 interPos, bool[] interDir) {}
}