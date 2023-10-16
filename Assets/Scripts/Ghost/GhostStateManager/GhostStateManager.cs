using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostStateManager : MonoBehaviour
{
    [SerializeField] private GhostStateSettings settings;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    private List<GhostStateManagerObserver> observers = new List<GhostStateManagerObserver>();
    private float time = 0.0f;
    private float frightenedDuration = 6.0f;
    private IEnumerator statesCoroutine;
    private IEnumerator waitFrightenedCoroutine;
    private int durationsLength;
    private int progress;

    private void Awake()
    {
        GetComponents<GhostStateManagerObserver>(observers);
        statesCoroutine = SetStates();
        durationsLength = settings.GetDurationsLenght();
        waitFrightenedCoroutine = WaitFrightenedState();
        powerPelletChannel.AddListener(EnableFrightenedState);
    }

    private void OnEnable()
    {
        StartCoroutine(statesCoroutine);
    }

    private IEnumerator SetStates()
    {
        for (progress = 0; progress < durationsLength; progress++)
        {
            NotifyObservers(progress);
            while (time < settings.GetDuration(progress))
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0.0f;
        }
        NotifyObservers(progress);
    }

    private void NotifyObservers(int index)
    {
        GhostStateAbstractFactory factory;

        factory = settings.GetStateFactory(index);
        NotifyObservers(factory);
    }

    private void NotifyObservers(GhostStateAbstractFactory factory)
    {
        foreach (GhostStateManagerObserver observer in observers)
        {
            observer.UpdateState(factory);
        }
    }

    private void EnableFrightenedState()
    {
        StopCoroutine(statesCoroutine);
        NotifyObservers(new GhostStateFrightenedFactory());
        if (waitFrightenedCoroutine is not null)
        {
            StopCoroutine(waitFrightenedCoroutine);
        }
        waitFrightenedCoroutine = WaitFrightenedState();
        StartCoroutine(waitFrightenedCoroutine);

        Debug.Log("ACTIVATED: FRIGHTENED");
    }

    private IEnumerator WaitFrightenedState()
    {
        yield return new WaitForSeconds(frightenedDuration);
        DisableFrightenedState();
    }

    private void DisableFrightenedState()
    {
        NotifyObservers(progress);
        StartCoroutine(statesCoroutine);

        Debug.Log("DISABLED: FRIGHTENED");
    }
}