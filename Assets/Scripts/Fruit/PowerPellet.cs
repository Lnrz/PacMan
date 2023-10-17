using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : AbstractPickable
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;

    protected override void OnPlayerDetection()
    {
        powerPelletChannel.Raise();
    }
}