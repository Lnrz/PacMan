using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GhostStateManager : MonoBehaviour, ChangeStateEventInvoker
{
    [SerializeField] private GhostStateSettings settings;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    private UnityEvent<GhostStateAbstractFactory> onChangeStateEvent = new UnityEvent<GhostStateAbstractFactory>();
    private IEnumerator statesCoroutine;
    private int durationsLength;
    private int progress = 0;
    private bool isFrightened = false;
    private bool isInOrGoingHome = true;

    private void Awake()
    {
        statesCoroutine = SetStates();
        durationsLength = settings.GetDurationsLenght();
        powerPelletChannel.AddListener(EnableFrightenedState);
        powerUpEndChannel.AddListener(DisableFrightenedState);
        gameRestartChannel.AddListener(OnGameRestart);
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
            FireChangeStateEvent(progress);
            StartCoroutine(statesCoroutine);
        }
        else
        {
            FireChangeStateEvent(new GhostStateFrightenedFactory());
        }
    }

    private void OnEaten()
    {
        isInOrGoingHome = true;
        isFrightened = false;
        FireChangeStateEvent(new GhostStateGoHomeFactory());
    }

    private void OnEnteringHome()
    {
        FireChangeStateEvent(new GhostStateDefaultFactory());
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
                yield return new WaitForEndOfFrame();
            }
            progress++;
            FireChangeStateEvent(progress);
        }
    }

    private void FireChangeStateEvent(int index)
    {
        GhostStateAbstractFactory factory;

        factory = settings.GetStateFactory(index);
        FireChangeStateEvent(factory);
    }

    private void FireChangeStateEvent(GhostStateAbstractFactory factory)
    {
        onChangeStateEvent.Invoke(factory);
    }

    private void EnableFrightenedState()
    {
        isFrightened = true;
        StopCoroutine(statesCoroutine);
        if (!isInOrGoingHome)
        {
            FireChangeStateEvent(new GhostStateFrightenedFactory());
        }
    }

    private void DisableFrightenedState()
    {
        if (isFrightened)
        {
            isFrightened = false;
            if (!isInOrGoingHome)
            {
                FireChangeStateEvent(progress);
                StartCoroutine(statesCoroutine);
            }

            Debug.Log("DISABLED: FRIGHTENED");
        }
    }

    public void OnChangeState(UnityAction<GhostStateAbstractFactory> listener)
    {
        onChangeStateEvent.AddListener(listener);
    }

    private void OnGameRestart()
    {
        isInOrGoingHome = true;
        isFrightened = false;
        progress = 0;
        StopCoroutine(statesCoroutine);
        statesCoroutine = SetStates();
    }
}