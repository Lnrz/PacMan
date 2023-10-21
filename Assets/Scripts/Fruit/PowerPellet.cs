using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : AbstractEatable
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;

    protected override void OnPlayerDetection()
    {
        powerPelletChannel.Raise();
    }
}