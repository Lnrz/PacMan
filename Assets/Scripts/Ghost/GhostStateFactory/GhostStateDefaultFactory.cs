using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateDefaultFactory : GhostStateAbstractFactory
{
    public GhostContactState GetContactState()
    {
        return new IgnoreGhostContactState();
    }

    public GhostMovementState GetMovementState()
    {
        return new StillGhostMovementState();
    }
}