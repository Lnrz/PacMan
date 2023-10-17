using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCounterManager : MonoBehaviour
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private int[] waitTimes = { 0, 30, 60};
    private int priority = 0;

    private void Awake()
    {
        dotChannel.AddListener(DecreaseWait);
    }

    private void Start()
    {
        if (waitTimes[priority] == 0)
        {
            WakeUpGhost();
        }
    }

    private void DecreaseWait()
    {
        waitTimes[priority]--;
        if (waitTimes[priority] == 0)
        {
            WakeUpGhost();
        }
    }

    private void WakeUpGhost()
    {
        exitHouseChannel.Invoke(priority);
        priority++;
        if (priority > 2)
        {
            dotChannel.RemoveListener(DecreaseWait);
        }
        else if (waitTimes[priority] == 0)
        {
            WakeUpGhost();
        }
    }
}