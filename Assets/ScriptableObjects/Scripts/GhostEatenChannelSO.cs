using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GhostEatenChannelSO", menuName = "ScriptableObjects/Channels/GhostEatenChannel", order = 1)]
public class GhostEatenChannelSO : ScriptableObject
{
    private UnityEvent listeners;

    public void AddListener(UnityAction listener)
    {
        listeners.AddListener(listener);
    }

    public void Invoke()
    {
        listeners?.Invoke();
    }
}