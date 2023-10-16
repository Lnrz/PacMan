using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PowerPelletChannelSO", menuName = "ScriptableObjects/Channels/PowerPelletChannel", order = 1)]
public class PowerPelletChannelSO : ScriptableObject
{
    private UnityEvent listenersEvent;

    public void Raise()
    {
        listenersEvent?.Invoke();
    }

    public void AddListener(UnityAction listener)
    {
        listenersEvent.AddListener(listener);
    }
}