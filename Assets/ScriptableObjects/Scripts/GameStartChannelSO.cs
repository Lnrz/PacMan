using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameStartChannelSO", menuName = "ScriptableObjects/Channels/GameStartChannel", order = 1)]
public class GameStartChannelSO : ScriptableObject
{
    private UnityEvent listeners;

    public void AddListener(UnityAction listener)
    {
        listeners.AddListener(listener);
    }

    public void Invoke()
    {
        listeners.Invoke();
    }
}