using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : AbstractPickable
{
    [SerializeField] private DotChannelSO dotChannel;

    protected override void OnPlayerDetection()
    {
        dotChannel.Invoke();
    }
}