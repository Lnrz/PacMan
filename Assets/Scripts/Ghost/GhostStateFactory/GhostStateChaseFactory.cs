using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateChaseFactory : GhostStateAbstractFactory
{
    public GhostContactState GetContactState()
    {
        return new EatGhostContactState();
    }

    public GhostMovementState GetMovementState()
    {
        return new ChaseGhostMovementState();
    }
}