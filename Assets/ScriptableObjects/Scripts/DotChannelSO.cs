using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DotChannelSO", menuName = "ScriptableObjects/Channels/DotChannel", order = 1)]
public class DotChannelSO : ScriptableObject
{
    private UnityEvent listeners;

    public void Invoke()
    {
        listeners?.Invoke();
    }

    public void AddListener(UnityAction listener)
    {
        listeners.AddListener(listener);
    }

    public void RemoveListener(UnityAction listener)
    {
        listeners.RemoveListener(listener);
    }
}