using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCounterManager : MonoBehaviour
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private int[] waitTimes = { 0, 5, 60};
    private int priority = 0;

    private void Awake()
    {
        dotChannel.AddListener(DecreaseWait);
        gameStartChannel.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        if (waitTimes[priority] == 0)
        {
            WakeUpGhost();
        }
    }

    private void DecreaseWait()
    {
        if (priority < 3)
        {
            waitTimes[priority]--;
            if (waitTimes[priority] == 0)
            {
                WakeUpGhost();
            }
        }
    }

    private void WakeUpGhost()
    {
        exitHouseChannel.Invoke(priority);
        priority++;
        if (priority < 3 && waitTimes[priority] == 0)
        {
            WakeUpGhost();
        }
    }
}