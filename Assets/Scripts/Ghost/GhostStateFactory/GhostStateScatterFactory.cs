using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateScatterFactory : GhostStateAbstractFactory
{
    public GhostContactState GetContactState()
    {
        return new EatGhostContactState();
    }

    public GhostMovementState GetMovementState()
    {
        return new ScatterGhostMovementState();
    }
}