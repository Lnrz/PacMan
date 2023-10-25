using UnityEngine;

public abstract class AbstractSettingsManager<SO, CH, S> : MonoBehaviour where SO : AbstractSettingsObject<S> where CH : EventChannelOneArg<S>
{
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;
    [SerializeField] private CH settingsChannel;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private SO[] settings;
    private int currentIndex = 0;

    private void Awake()
    {
        nextLevelChannel.AddListener(OnNextLevel);
        gameEndChannel.AddListener(OnGameEnd);
    }

    private void Start()
    {
        UpdateIndex();
        UpdateSettings();
    }

    private void UpdateIndex()
    {
        while (!settings[currentIndex].IsBetweenLevels(currentLevel))
        {
            currentIndex++;
        }
    }

    private void UpdateSettings()
    {
        S newSettings;

        newSettings = settings[currentIndex].GetSettings();
        settingsChannel.Invoke(newSettings);
    }

    private void OnNextLevel()
    {
        int oldIndex;

        currentLevel++;
        oldIndex = currentIndex;
        UpdateIndex();
        if (oldIndex != currentIndex)
        {
            UpdateSettings();
        }
    }

    private void OnGameEnd()
    {
        currentIndex = 0;
        currentLevel = 1;
    }
}