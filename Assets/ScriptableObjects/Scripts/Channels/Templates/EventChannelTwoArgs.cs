using UnityEngine;
using UnityEngine.Events;

public class EventChannelTwoArgs<T1, T2> : ScriptableObject
{
    private UnityEvent<T1, T2> listeners = new UnityEvent<T1, T2>();

    public void AddListener(UnityAction<T1, T2> listener)
    {
        listeners.AddListener(listener);
    }

    public void RemoveListener(UnityAction<T1, T2> listener)
    {
        listeners.RemoveListener(listener);
    }

    public void Invoke(T1 arg1, T2 arg2)
    {
        listeners.Invoke(arg1, arg2);
    }
}
