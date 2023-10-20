using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GhostHouse : MonoBehaviour, OutsideHomeEventInvoker, EnteringHomeEventInvoker
{
    [SerializeField] private ExitHouseChannelSO exitHouseChannel;
    [SerializeField] private GhostHouseSettings settings;
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
    private float maxDistFromHouseExit = 0.001f;

    private void Awake()
    {
        ghostHouseCenter = settings.GetGhostHouseCenter();
        ghostHouseExit = settings.GetExitPosition();
        if (TryGetComponent<EatenEventInvoker>(out EatenEventInvoker eatenEventInvoker))
        {
            eatenEventInvoker.OnEaten(OnEaten);
        }
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
            startPos = -1;
            transform.position = ghostHouseExit;
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
        return ((Vector2)transform.position - ghostHouseExit).sqrMagnitude <= maxDistFromHouseExit;
    }

    private void OnEaten()
    {
        isGoingHome = true;
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
                yield return new WaitForEndOfFrame();
            }
        }
        time = 0;
        while (time < 1)
        {
            newPos = Vector2.Lerp(ghostHouseCenter, ghostHouseExit, time);
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
        while (time < 1)
        {
            newPos = Vector2.Lerp(ghostHouseExit, ghostHouseCenter, time);
            transform.position = newPos;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        if (startPos != -1)
        {
            while (time < 0.5)
            {
                newPos = Vector2.Lerp(ghostHouseCenter, ghostPositionInHouse, time * 2.0f);
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
}