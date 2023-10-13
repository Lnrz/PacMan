using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FruitChannelSO", menuName = "ScriptableObjects/Channels/FruitChannel", order = 1)]
public class FruitChannelSO : ScriptableObject
{
    private UnityEvent<int> listenersEvent;

    public void Raise(int points)
    {
        listenersEvent?.Invoke(points);
    }

    public void AddListener(UnityAction<int> listener)
    {
        listenersEvent.AddListener(listener);
    }
}