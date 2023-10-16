using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateFrightenedFactory : GhostStateAbstractFactory
{
    public GhostContactState GetContactState()
    {
        return new FrightenedGhostContactState();
    }

    public GhostMovementState GetMovementState()
    {
        return new FrightenedGhostMovementState();
    }
}