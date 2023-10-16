using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostStateSettings", menuName = "ScriptableObjects/GhostStateSettings", order = 1)]
public class GhostStateSettings : ScriptableObject
{
    private GhostStateAbstractFactory[] states;
    private float[] durations;

    //Da renderlo settabile usando un menu custom
    private void OnValidate()
    {
        durations = new float[3] { 10.0f, 20.0f, 7.0f };
        states = new GhostStateAbstractFactory[4] { new GhostStateScatterFactory(), new GhostStateChaseFactory(), new GhostStateScatterFactory(), new GhostStateChaseFactory() };
    }

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
        return states[index];
    }
}