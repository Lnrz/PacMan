using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : AbstractEatable
{
    [SerializeField] private DotChannelSO dotChannel;

    protected override void OnPlayerDetection()
    {
        dotChannel.Invoke();
    }
}