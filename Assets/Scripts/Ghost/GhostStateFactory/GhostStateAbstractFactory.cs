using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GhostStateAbstractFactory
{
    public GhostMovementState GetMovementState();

    public GhostContactState GetContactState();
}