using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GhostHouse : MonoBehaviour
{
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GhostHouseSettings settings;
    [SerializeField] private int priority = 0;
    [SerializeField] private int startPos = -1;
    [SerializeField] private bool isAlreadyOut = false;
    private List<GhostHouseDependedActivation> ghostHouseDAScripts = new List<GhostHouseDependedActivation>();
    private IEnumerator exitHouseCorout;
    private Vector2 ghostHouseCenter;
    private Vector2 ghostHouseExit;
    private Vector2 ghostPositionInHouse;


    private void Awake()
    {
        GetComponents<GhostHouseDependedActivation>(ghostHouseDAScripts);
        ghostHouseCenter = settings.GetGhostHouseCenter();
        ghostHouseExit = settings.GetExitPosition();
        if (!isAlreadyOut)
        {
            exitHouseChannel.AddListener(WakeUp);
            exitHouseCorout = ExitHouse();
            if (settings.IsVertical() && startPos != -1)
            {
                startPos = (startPos + 1) % 4;
            }
            ghostPositionInHouse = ghostHouseCenter + Utility.Int2Dir(startPos);
            transform.position = ghostPositionInHouse;
        }
        else
        {
            transform.position = ghostHouseExit;
        }
    }

    private void WakeUp(int priority)
    {
        if (this.priority == priority)
        {
            StartCoroutine(exitHouseCorout);    
        }
    }

    private IEnumerator ExitHouse()
    {
        Vector2 newPos;
        float time;

        time = 0;
        if (startPos != -1)
        {
            while (time < 0.5)
            {
                newPos = Vector2.Lerp(ghostPositionInHouse, ghostHouseCenter, time * 2.0f);
                transform.position = newPos;
                time += Time.deltaTime;
                yield return null;
            }
        }
        time = 0;
        while (time < 1)
        {
            newPos = Vector2.Lerp(ghostHouseCenter, ghostHouseExit, time);
            transform.position = newPos;
            time += Time.deltaTime;
            yield return null;
        }
        foreach (GhostHouseDependedActivation script in ghostHouseDAScripts)
        {
            script.Activate();
        }
    }

    private IEnumerator EnterHouse()
    {
        Vector2 newPos;
        float time;

        time = 0;
        while (time < 1)
        {
            newPos = Vector2.Lerp(ghostHouseExit, ghostHouseCenter, time);
            transform.position = newPos;
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        if (startPos != -1)
        {
            while (time < 0.5)
            {
                newPos = Vector2.Lerp(ghostHouseCenter, ghostPositionInHouse, time * 2.0f);
                transform.position = newPos;
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}