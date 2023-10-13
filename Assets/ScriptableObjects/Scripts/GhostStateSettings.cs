using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostStateSettings", menuName = "ScriptableObjects/GhostStateSettings", order = 1)]
public class GhostStateSettings : ScriptableObject
{
    private Type[] states;
    private float[] durations;

    //Da renderlo settabile usando un menu custom
    private void OnValidate()
    {
        durations = new float[3] { 7.0f, 20.0f, 5.0f };
        states = new Type[4] { typeof(ScatterGhostState), typeof(ChaseGhostState), typeof(ScatterGhostState), typeof(ChaseGhostState) };
    }

    public int GetDurationsLenght()
    {
        return durations.Length;
    }

    public float GetDuration(int index)
    {
        return durations[index];
    }

    public Type GetStateType(int index)
    {
        return states[index];
    }
}