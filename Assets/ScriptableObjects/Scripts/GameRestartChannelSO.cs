using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameRestartChannelSO", menuName = "ScriptableObjects/Channels/GameRestartChannel", order = 1)]
public class GameRestartChannelSO : ScriptableObject
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