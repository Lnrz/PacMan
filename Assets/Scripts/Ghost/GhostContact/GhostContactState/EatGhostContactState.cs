using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatGhostContactState : GhostContactState
{
    public override void OnContactWithPlayer(GameObject player)
    {
        if (player.TryGetComponent<PlayerLifeManager>(out PlayerLifeManager plm))
        {
            plm.DecreaseLivesLeft();
        }
    }
}