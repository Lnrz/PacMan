using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GhostHouse : MonoBehaviour, OutsideHomeEventInvoker, EnteringHomeEventInvoker, InsideHomeEventInvoker, ExitingHomeEventInvoker
{
    private static readonly float maxDistFromHouseExit = 0.035f;
    private static readonly float enterExitSpeed = 2.5f;
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GhostHouseSettings settings;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private StopEntitiesChannelSO stopEntitiesChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private int priority = 0;
    [SerializeField] private int startPos = -1;
    [SerializeField] private bool isAlreadyOut = false;
    private UnityEvent outsideHomeEvent = new UnityEvent();
    private UnityEvent insideHomeEvent = new UnityEvent();
    private UnityEvent enteringHomeEvent = new UnityEvent();
    private UnityEvent exitingHomeEvent = new UnityEvent();
    private LookController lookControl;
    private IEnumerator exitHouseCorout;
    private IEnumerator enterHouseCorout;
    private Vector2 ghostHouseCenter;
    private Vector2 ghostHouseExit;
    private Vector2 ghostPositionInHouse;
    private int exitPosLookInd = -1;
    private int exitHouseLookInd = -1;
    private int enterPosLookInd = -1;
    private int enterHouseLookInd = -1;
    private bool isGoingHome = false;

    private void Awake()
    {
        ghostHouseCenter = settings.GetGhostHouseCenter();
        ghostHouseExit = settings.GetExitPosition();
        gameStartChannel.AddListener(OnStart);
        stopEntitiesChannel.AddListener(OnStopEntities);
        gameRestartChannel.AddListener(OnGameRestart);
        if (TryGetComponent<EatenEventInvoker>(out EatenEventInvoker eatenEventInvoker))
        {
            eatenEventInvoker.OnEaten(OnEaten);
        }
        if (!isAlreadyOut)
        {
            exitHouseChannel.AddListener(WakeUp);
            if (settings.IsVertical())
            {
                startPos = MyDirUtils.GetNextDirectionIndex(startPos);
            }
            ghostPositionInHouse = ghostHouseCenter + 2.0f * MyDirUtils.Int2Dir(startPos);
            transform.position = ghostPositionInHouse;
        }
        else
        {
            startPos = -1;
            ghostPositionInHouse = ghostHouseCenter;
            transform.position = ghostHouseExit;
        }
        lookControl = GetComponent<LookController>();
        exitHouseLookInd = MyDirUtils.Dir2Int(ghostHouseExit - ghostHouseCenter);
        enterHouseLookInd = MyDirUtils.GetOppositeDirectionIndex(exitHouseLookInd);
        if (startPos != -1)
        {
            exitPosLookInd = MyDirUtils.GetOppositeDirectionIndex(startPos);
            enterPosLookInd = startPos;
        }
    }

    private void OnStart()
    {
        if (isAlreadyOut)
        {
            FireOutsideHomeEvent();
        }
    }

    private void Update()
    {
        if (isGoingHome && IsCloseToHomeExit())
        {
            isGoingHome = false;
            enterHouseCorout = EnterHouse();
            StartCoroutine(enterHouseCorout);
        }
    }

    private bool IsCloseToHomeExit()
    {
        Vector2 dist;

        dist = (Vector2)transform.position - ghostHouseExit;
        return dist.sqrMagnitude <= maxDistFromHouseExit;
    }

    private void OnEaten()
    {
        isGoingHome = true;
    }

    private void WakeUp(int priority)
    {
        if (this.priority == priority)
        {
            exitHouseCorout = ExitHouse();
            StartCoroutine(exitHouseCorout);
        }
    }

    private IEnumerator ExitHouse()
    {
        Vector2 newPos;
        float time;

        FireExitingHomeEvent();
        time = 0;
        if (startPos != -1)
        {
            lookControl.Look(exitPosLookInd);
            while (time < 2 / enterExitSpeed)
            {
                newPos = Vector2.Lerp(ghostPositionInHouse, ghostHouseCenter, time / 2 * enterExitSpeed);
                transform.position = newPos;
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        lookControl.Look(exitHouseLookInd);
        time = 0;
        while (time < 4 / enterExitSpeed)
        {
            newPos = Vector2.Lerp(ghostHouseCenter, ghostHouseExit, time / 4 * enterExitSpeed);
            transform.position = newPos;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = ghostHouseExit;
        FireOutsideHomeEvent();
    }

    private IEnumerator EnterHouse()
    {
        Vector2 newPos;
        float time;

        FireEnteringHomeEvent();
        time = 0;
        lookControl.Look(enterHouseLookInd);
        while (time < 4 / enterExitSpeed)
        {
            newPos = Vector2.Lerp(ghostHouseExit, ghostHouseCenter, time / 4 * enterExitSpeed);
            transform.position = newPos;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        if (startPos != -1)
        {
            lookControl.Look(enterPosLookInd);
            while (time < 2 / enterExitSpeed)
            {
                newPos = Vector2.Lerp(ghostHouseCenter, ghostPositionInHouse, time / 2 * enterExitSpeed);
                transform.position = newPos;
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        FireInsideHomeEvent();
        exitHouseCorout = ExitHouse();
        StartCoroutine(exitHouseCorout);
    }

    public void OnOutsideHome(UnityAction listener)
    {
        outsideHomeEvent.AddListener(listener);
    }

    private void FireOutsideHomeEvent()
    {
        outsideHomeEvent.Invoke();
    }

    public void OnEnteringHome(UnityAction listener)
    {
        enteringHomeEvent.AddListener(listener);
    }

    private void FireEnteringHomeEvent()
    {
        enteringHomeEvent.Invoke();
    }

    private void OnStopEntities()
    {
        isGoingHome = false;
        StopAllCoroutines();
    }

    private void OnGameRestart()
    {
        if (!isAlreadyOut)
        {
            transform.position = ghostPositionInHouse;
        }
        else
        {
            transform.position = ghostHouseExit;
        }
    }

    public void OnInsideHome(UnityAction listener)
    {
        insideHomeEvent.AddListener(listener);
    }

    private void FireInsideHomeEvent()
    {
        insideHomeEvent.Invoke();
    }

    public void OnExitingHome(UnityAction listener)
    {
        exitingHomeEvent.AddListener(listener);
    }

    private void FireExitingHomeEvent()
    {
        exitingHomeEvent.Invoke();
    }
}