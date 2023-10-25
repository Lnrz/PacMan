using UnityEngine;
using UnityEngine.Events;

public abstract class EventChannelOneArg<T> : ScriptableObject
{
    private UnityEvent<T> listeners = new UnityEvent<T>();

    public void AddListener(UnityAction<T> listener)
    {
        listeners.AddListener(listener);
    }

    public void RemoveListener(UnityAction<T> listener)
    {
        listeners.RemoveListener(listener);
    }

    public void Invoke(T arg)
    {
        listeners.Invoke(arg);
    }
}