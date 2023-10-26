using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GhostHouse : MonoBehaviour, OutsideHomeEventInvoker, EnteringHomeEventInvoker
{
    private static readonly float maxDistFromHouseExit = 0.035f;
    private static readonly float enterExitSpeed = 2.5f;
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GhostHouseSettings settings;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private int priority = 0;
    [SerializeField] private int startPos = -1;
    [SerializeField] private bool isAlreadyOut = false;
    private UnityEvent outsideHomeEvent = new UnityEvent();
    private UnityEvent enteringHomeEvent = new UnityEvent();
    private IEnumerator exitHouseCorout;
    private IEnumerator enterHouseCorout;
    private Vector2 ghostHouseCenter;
    private Vector2 ghostHouseExit;
    private Vector2 ghostPositionInHouse;
    private bool isGoingHome = false;

    private void Awake()
    {
        ghostHouseCenter = settings.GetGhostHouseCenter();
        ghostHouseExit = settings.GetExitPosition();
        gameStartChannel.AddListener(OnStart);
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
                startPos = Utility.GetNextDirectionIndex(startPos);
            }
            ghostPositionInHouse = ghostHouseCenter + Utility.Int2Dir(startPos);
            transform.position = ghostPositionInHouse;
        }
        else
        {
            startPos = -1;
            ghostPositionInHouse = ghostHouseCenter;
            transform.position = ghostHouseExit;
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
            FireEnteringHomeEvent();
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

        time = 0;
        if (startPos != -1)
        {
            while (time < 1 / enterExitSpeed)
            {
                newPos = Vector2.Lerp(ghostPositionInHouse, ghostHouseCenter, time * enterExitSpeed);
                transform.position = newPos;
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        time = 0;
        while (time < 2 / enterExitSpeed)
        {
            newPos = Vector2.Lerp(ghostHouseCenter, ghostHouseExit, time / 2 * enterExitSpeed);
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

        time = 0;
        while (time < 2 / enterExitSpeed)
        {
            newPos = Vector2.Lerp(ghostHouseExit, ghostHouseCenter, time / 2 * enterExitSpeed);
            transform.position = newPos;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        if (startPos != -1)
        {
            while (time < 1 / enterExitSpeed)
            {
                newPos = Vector2.Lerp(ghostHouseCenter, ghostPositionInHouse, time * enterExitSpeed);
                transform.position = newPos;
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
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

    private void OnGameRestart()
    {
        isGoingHome = false;
        StopAllCoroutines();
        if (!isAlreadyOut)
        {
            transform.position = ghostPositionInHouse;
        }
        else
        {
            transform.position = ghostHouseExit;
        }
    }
}