using System;
using UnityEngine;

public class GhostCounterManager : MonoBehaviour
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GhostHouseWaitSettingsChannelSO ghostHouseWaitSettingsChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    private int[] localCounter = new int[3];
    private int[] globalCounter = new int[3];
    private int[] localCounterCopy = new int[3];
    private int[] globalCounterCopy = new int[3];
    private int[] actualCounter;
    private int priority = 0;
    private bool isGameStarted = false;

    private void Awake()
    {
        actualCounter = localCounterCopy;
        dotChannel.AddListener(DecreaseWait);
        ghostHouseWaitSettingsChannel.AddListener(OnWaitHouseSettingsChange);
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
        nextLevelChannel.AddListener(OnNextLevel);
    }

    private void OnWaitHouseSettingsChange(GhostHouseWaitSettingsSO ghostHouseWaitSettings)
    {
        Array.Copy(ghostHouseWaitSettings.GetLocalCounter(), localCounter, 3);
        Array.Copy(localCounter, localCounterCopy, 3);
        Array.Copy(ghostHouseWaitSettings.GetGlobalCounter(), globalCounter, 3);
        Array.Copy(globalCounter, globalCounterCopy, 3);
    }

    private void OnGameStart()
    {
        isGameStarted = true;
        if (actualCounter[priority] <= 0)
        {
            WakeUpGhost();
        }
    }

    private void OnGameRestart()
    {
        isGameStarted = false;
        priority = 0;
        Array.Copy(globalCounter, globalCounterCopy, 3);
        actualCounter = globalCounterCopy;
    }

    private void OnNextLevel()
    {
        isGameStarted = false;
        priority = 0;
        Array.Copy(localCounter, localCounterCopy, 3);
        actualCounter = localCounterCopy;
    }

    private void DecreaseWait()
    {
        if (isGameStarted && priority < 3)
        {
            actualCounter[priority]--;
            if (actualCounter[priority] <= 0)
            {
                WakeUpGhost();
            }
        }
    }

    private void WakeUpGhost()
    {
        exitHouseChannel.Invoke(priority);
        priority++;
        if (priority < 3 && actualCounter[priority] <= 0)
        {
            WakeUpGhost();
        }
    }
}