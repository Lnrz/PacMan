using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpDurationSettingsSO", menuName = "ScriptableObjects/Settings/PowerUpDurationSettings", order = 1)]
public class PowerUpDurationSettings : ScriptableObject
{
    [SerializeField] private float duration;

    public float GetDuration()
    {
        return duration;
    }
}