using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PowerUpEndChannelSO", menuName = "ScriptableObjects/Channels/PowerUpEndChannel", order = 1)]
public class PowerUpEndChannelSO : ScriptableObject
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