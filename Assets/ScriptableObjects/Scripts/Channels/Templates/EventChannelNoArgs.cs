using UnityEngine;
using UnityEngine.Events;

public abstract class EventChannelNoArgs : ScriptableObject
{
    private UnityEvent listeners = new UnityEvent();

    public void AddListener(UnityAction listener)
    {
        listeners.AddListener(listener);
    }

    public void RemoveListener(UnityAction listener)
    {
        listeners.RemoveListener(listener);
    }

    public void Invoke()
    {
        listeners.Invoke();
    }
}