using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightenedGhostContactState : GhostContactState
{
    public override void OnContactWithPlayer(GameObject ghost, GameObject player)
    {
        ghost.SetActive(false);
    }
}