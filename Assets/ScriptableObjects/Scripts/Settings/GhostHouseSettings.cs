using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostHouseSettings", menuName = "ScriptableObjects/Settings/GhostHouseSettings", order = 1)]
public class GhostHouseSettings : ScriptableObject
{
    [SerializeField] private Vector2 ghostHouseCenter;
    [SerializeField] private Vector2 exitPosition;
    [SerializeField] private bool isVertical;
    [SerializeField] private bool[] turnableDirOutside = { false, false, false, false};

    public Vector2 GetGhostHouseCenter()
    {
        return ghostHouseCenter;
    }

    public Vector2 GetExitPosition()
    {
        return exitPosition;
    }

    public bool IsVertical()
    {
        return isVertical;
    }

    public bool[] GetTurnableDirsOutside()
    {
        bool[] res = new bool[4];

        Array.Copy(turnableDirOutside, res, 4);
        return res;
    }
}