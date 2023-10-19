using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateManager : MonoBehaviour
{
    [SerializeField] private GhostStateSettings settings;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    private List<GhostStateManagerObserver> observers = new List<GhostStateManagerObserver>();
    private float frightenedDuration = 6.0f;
    private IEnumerator statesCoroutine;
    private IEnumerator waitFrightenedCoroutine;
    private int durationsLength;
    private int progress = 0;
    private bool isFrightened = false;
    private bool isInOrGoingHome = true;

    private void Awake()
    {
        GetComponents<GhostStateManagerObserver>(observers);
        statesCoroutine = SetStates();
        durationsLength = settings.GetDurationsLenght();
        powerPelletChannel.AddListener(EnableFrightenedState);
        if (TryGetComponent<OutsideHomeEventInvoker>(out OutsideHomeEventInvoker outsideHomeEventInvoker))
        {
            outsideHomeEventInvoker.OnOutsideHome(OnOutsideHome);
        }
        if (TryGetComponent<EatenEventInvoker>(out EatenEventInvoker eatenEventInvoker))
        {
            eatenEventInvoker.OnEaten(OnEaten);
        }
        if (TryGetComponent<EnteringHomeEventInvoker>(out EnteringHomeEventInvoker enteringHomeEventInvoker))
        {
            enteringHomeEventInvoker.OnEnteringHome(OnEnteringHome);
        }
    }

    private void OnOutsideHome()
    {
        isInOrGoingHome = false;
        if (!isFrightened)
        {
            NotifyObservers(progress);
            StartCoroutine(statesCoroutine);
        }
        else
        {
            NotifyObservers(new GhostStateFrightenedFactory());
        }
    }

    private void OnEaten()
    {
        isInOrGoingHome = true;
        isFrightened = false;
        StopCoroutine(waitFrightenedCoroutine);
        NotifyObservers(new GhostStateGoHomeFactory());
    }

    private void OnEnteringHome()
    {
        NotifyObservers(new GhostStateDefaultFactory());
    }

    private IEnumerator SetStates()
    {
        float time;
        
        while (progress < durationsLength)
        {
            time = 0.0f;
            while (time < settings.GetDuration(progress))
            {
                time += Time.deltaTime;
                yield return null;
            }
            progress++;
            NotifyObservers(progress);
        }
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
        isFrightened = true;
        StopCoroutine(statesCoroutine);
        if (!isInOrGoingHome)
        {
            NotifyObservers(new GhostStateFrightenedFactory());
        }
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
        isFrightened = false;
        if (!isInOrGoingHome)
        {
            NotifyObservers(progress);
            StartCoroutine(statesCoroutine);
        }

        Debug.Log("DISABLED: FRIGHTENED");
    }
}