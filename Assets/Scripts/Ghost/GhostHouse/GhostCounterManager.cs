using System;
using UnityEngine;

public class GhostCounterManager : MonoBehaviour
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private int[] localCounter = { 0, 30, 60 };
    [SerializeField] private int[] globalCounter = { 7, 10, 15 };
    private int[] localCounterCopy = new int[3];
    private int[] globalCounterCopy = new int[3];
    private int[] actualCounter = new int[3];
    private int priority = 0;

    private void Awake()
    {
        Array.Copy(localCounter, localCounterCopy, 3);
        actualCounter = localCounterCopy;
        dotChannel.AddListener(DecreaseWait);
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
    }

    private void OnGameStart()
    {
        if (actualCounter[priority] <= 0)
        {
            WakeUpGhost();
        }
    }

    private void OnGameRestart()
    {
        priority = 0;
        Array.Copy(globalCounter, globalCounterCopy, 3);
        actualCounter = globalCounterCopy;
    }

    private void DecreaseWait()
    {
        if (priority < 3)
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