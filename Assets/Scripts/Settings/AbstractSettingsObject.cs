using UnityEngine;

public abstract class AbstractSettingsObject<T>
{
    [SerializeField] private int startingLevel;
    [SerializeField] private int endLevel;
    [SerializeField] private T settings;

    public bool IsBetweenLevels(int level)
    {
        return level >= startingLevel && (endLevel == -1 || level <= endLevel);
    }

    public T GetSettings()
    {
        return settings;
    }
}