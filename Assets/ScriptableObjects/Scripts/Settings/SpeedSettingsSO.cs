using UnityEngine;

[CreateAssetMenu(fileName = "SpeedSettingsSO", menuName = "ScriptableObjects/Settings/SpeedSeetings", order = 1)]
public class SpeedSettingsSO : ScriptableObject
{
    [SerializeField] private float playerNormalSpeedMod;
    [SerializeField] private float playerPoweredSpeedMod;
    [SerializeField] private float ghostNormalSpeedMod;
    [SerializeField] private float ghostFrightenedSpeedMod;
    [SerializeField] private float ghostTunnelSpeedMod;

    public float GetPlayerNormalSpeedMod()
    {
        return playerNormalSpeedMod;
    }

    public float GetPlayerPoweredSpeedMod()
    {
        return playerPoweredSpeedMod;
    }

    public float GetGhostNormalSpeedMod()
    {
        return ghostNormalSpeedMod;
    }

    public float GetGhostFrightenedSpeedMod()
    {
        return ghostFrightenedSpeedMod;
    }

    public float GetGhostTunnelSpeedMod()
    {
        return ghostTunnelSpeedMod;
    }
}