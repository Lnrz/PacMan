using UnityEngine;

public class FrightenedGhostContactState : GhostContactState
{
    public override void OnContactWithPlayer(GameObject player)
    {
        context.FireOnEatenEvent();
    }
}