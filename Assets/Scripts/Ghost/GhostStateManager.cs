using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostStateManager : MonoBehaviour
{
    [SerializeField] private GhostStateSettings settings;
    private float time = 0.0f;
    private GhostMovement gm;
    private IEnumerator statesCoroutine;
    private int durationsLength;

    private void Awake()
    {
        gm = GetComponent<GhostMovement>();
        statesCoroutine = SetStates();
        durationsLength = settings.GetDurationsLenght();
    }

    private void OnEnable()
    {
        gm.enabled = true;
        StartCoroutine(statesCoroutine);
    }

    private IEnumerator SetStates()
    {
        for (int i = 0; i < durationsLength; i++)
        {
            gm.ChangeState(GetGhostState(i));
            while (time < settings.GetDuration(i))
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
        }
        gm.ChangeState(GetGhostState(durationsLength));
    }

    private GhostState GetGhostState(int index)
    {
        return (GhostState)Activator.CreateInstance(settings.GetStateType(index));
    }
}