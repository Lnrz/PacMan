using UnityEngine;

[CreateAssetMenu(fileName = "GhostStateSettings", menuName = "ScriptableObjects/Settings/GhostStateSettings", order = 1)]
public class GhostStateSettings : ScriptableObject
{
    [SerializeField] private int[] statesIndex;
    [SerializeField] private float[] durations;
    private GhostStateAbstractFactory[] states = { new GhostStateScatterFactory(), new GhostStateChaseFactory() };

    public int GetDurationsLenght()
    {
        return durations.Length;
    }

    public float GetDuration(int index)
    {
        return durations[index];
    }

    public GhostStateAbstractFactory GetStateFactory(int index)
    {
        return states[statesIndex[index]];
    }
}