using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    [SerializeField] private bool[] legalDir = { false, false, false, false };
    [SerializeField] private float speed = 2.0f;
    private int directionIndex = -1;
    private int newDirectionIndex = -1;
    private int nextDirectionIndex = -1;
    private float turnDist = 0.025f;
    private Vector3 direction = Vector3.zero;
    private Vector3 newDirection = Vector3.zero;
    private Vector3 nextDirection = Vector3.zero;

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
    private void Update()
    {
        newDirectionIndex = -1;

        GetDirectionInput();
        if (IsNewDirectionValid())
        {
            nextDirection = newDirection;
            nextDirectionIndex = newDirectionIndex;
        }
        if (IsNextDirectionPossible())
        {
            direction = nextDirection;
            directionIndex = nextDirectionIndex;
            AdjustPositionToDir();
            legalDir[(directionIndex + 2) % 4] = true; // This line makes legal the direction opposite to the direction in which the player is currently going
            nextDirectionIndex = -1;
        }
        transform.position += speed * Time.deltaTime * direction;
    }

    public bool GetIsLegalDir(int i)
    {
        return legalDir[i];
    }

    public int getDirectionIndex()
    {
        return directionIndex;
    }

    public void Stop()
    {
        direction = Vector3.zero;
        directionIndex = -1;
    }

    public void LockDirection(int directionIndex, bool lockMode)
    {
        legalDir[directionIndex] = !lockMode;
    }

    // Aligns the player position to the grid, meaning that his x and y values will be set at (NearestLowerinteger).5
    public void AdjustPosition()
    {
        Vector3 pos;

        pos = transform.position;
        pos.x = MathF.Floor(pos.x) + 0.5f;
        pos.y = MathF.Floor(pos.y) + 0.5f;
        transform.position = pos;
    }

    private void GetDirectionInput()
    {
        if (UpInput())
        {
            newDirection = Vector3.up;
            newDirectionIndex = 0;
        }
        else if (RightInput())
        {
            newDirection = Vector3.right;
            newDirectionIndex = 1;
        }
        else if (DownInput())
        {
            newDirection = Vector3.down;
            newDirectionIndex = 2;
        }
        else if (LeftInput())
        {
            newDirection = Vector3.left;
            newDirectionIndex = 3;
        }
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

    private bool IsNewDirectionValid()
    {
        return newDirectionIndex != -1 && legalDir[newDirectionIndex];
    }

    private bool IsNextDirectionPossible()
    {
        if (nextDirectionIndex == -1) return false; 
        float dist;

        if (nextDirectionIndex % 2 == 0)
        {
            dist = transform.position.x - Mathf.Floor(transform.position.x);
        }
        else
        {
            dist = transform.position.y - Mathf.Floor(transform.position.y);
        }
        dist = Mathf.Abs(dist - 0.5f);

        return
            (nextDirectionIndex % 2 == directionIndex % 2) ||
            dist <= turnDist;
    }


    // Aligns the player on the x or y axis of the grid, the axis is chosen based on the player direction
    private void AdjustPositionToDir()
    {
        Vector3 pos;

        pos = transform.position;
        if (directionIndex % 2 == 0)
        {
            pos.x = Mathf.Floor(transform.position.x) + 0.5f;
        }
        else
        {
            pos.y = Mathf.Floor(transform.position.y) + 0.5f;
        }
        transform.position = pos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<IntersectionStop>().ApplyDuringCollision(this);
    }
}