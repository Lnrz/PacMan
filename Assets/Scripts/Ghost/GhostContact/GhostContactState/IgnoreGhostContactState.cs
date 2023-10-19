using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreGhostContactState : GhostContactState
{
    public override void OnContactWithPlayer(GameObject player) {}
}