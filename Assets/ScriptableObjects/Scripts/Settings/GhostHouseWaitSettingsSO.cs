using UnityEngine;

[CreateAssetMenu(fileName = "GhostHouseWaitSettingsSO", menuName = "ScriptableObjects/Settings/GhostHouseWaitSettings", order = 1)]
public class GhostHouseWaitSettingsSO : ScriptableObject
{
    [SerializeField] private int[] localCounter = { 0, 0, 0 };
    [SerializeField] private int[] globalCounter = { 0, 0, 0};

    public int[] GetLocalCounter()
    {
        return localCounter;
    }

    public int[] GetGlobalCounter()
    {
        return globalCounter;
    }
}