using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateGoHomeFactory : GhostStateAbstractFactory
{
    public GhostContactState GetContactState()
    {
        return new IgnoreGhostContactState();
    }

    public GhostMovementState GetMovementState()
    {
        return new GoHomeGhostMovementState();
    }
}