using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatGhostContactState : GhostContactState
{
    public override void OnContactWithPlayer(GameObject player)
    {
        player.SetActive(false);
    }
}