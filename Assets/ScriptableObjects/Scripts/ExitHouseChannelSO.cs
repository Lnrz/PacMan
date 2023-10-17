using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ExitHouseChannelSO", menuName = "ScriptableObjects/Channels/ExitHouseChannel", order = 1)]
public class ExitHouseChannelSO : ScriptableObject
{
    private UnityEvent<int> listeners;

    public void Invoke(int priority)
    {
        listeners?.Invoke(priority);
    }

    public void AddListener(UnityAction<int> listener)
    {
        listeners.AddListener(listener);
    }
}