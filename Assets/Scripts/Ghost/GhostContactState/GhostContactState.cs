using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostContactState
{
    public abstract void OnContactWithPlayer(GameObject ghost, GameObject player);
}