using System.Collections;
using UnityEngine;

public class PlayerMovement : AbstractMovingEntity
{
    [SerializeField] private bool[] startDirections = { false, false, false, false };
    [SerializeField] private Vector2 startPos;
    [SerializeField] private float normalSpeedMod = 0.8f;
    [SerializeField] private float poweredSpeedMod = 0.9f;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    private float currentSpeedMod;
    private bool isEating = false;
    private bool isGameStarted = false;

    protected override void AwakeHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            LockDirection(i, !startDirections[i]);
        }
        transform.position = startPos;
        powerPelletChannel.AddListener(ChangeSpeedModToPowered);
        powerUpEndChannel.AddListener(ChangeSpeedModToNormal);
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
        ChangeSpeedModToNormal();
        if (TryGetComponent<PlayerInputEventInvoker>(out PlayerInputEventInvoker playerInputEventInvoker))
        {
            playerInputEventInvoker.OnPlayerInputEvent(OnPlayerInput);
        }
    }

    private void OnGameStart()
    {
        isGameStarted = true;
    }

    private void OnPlayerInput(int inputDirectionIndex)
    {
        if (isGameStarted)
        {
            ChangeDirection(inputDirectionIndex);
        }
    }

    private void ChangeSpeedModToPowered()
    {
        currentSpeedMod = poweredSpeedMod;
    }

    private void ChangeSpeedModToNormal()
    {
        currentSpeedMod = normalSpeedMod;
        if (!isEating)
        {
            ChangeSpeedMod(currentSpeedMod);
        }
    }

    public void OnEating(float eatingTime, float eatingSpeedReduction)
    {
        IEnumerator onEatingCorout;

        onEatingCorout = OnEatingCoroutine(eatingTime, eatingSpeedReduction);
        StartCoroutine(onEatingCorout);
    }

    private IEnumerator OnEatingCoroutine(float eatingTime, float eatingSpeedReduction)
    {
        float speedMod;

        isEating = true;
        speedMod = Mathf.Max(0, currentSpeedMod - eatingSpeedReduction);
        ChangeSpeedMod(speedMod);
        yield return new WaitForSeconds(eatingTime);
        ChangeSpeedMod(currentSpeedMod);
        isEating = false;
    }

    private void OnGameRestart()
    {
        transform.position = startPos;
        isGameStarted = false;
        isEating = false;
        ChangeSpeedModToNormal();
        for (int i = 0; i < 4; i++)
        {
            LockDirection(i, !startDirections[i]);
        }
    }

    public override sealed void IntersectionStopEnter(Vector3 interPos) {} 

    protected override void UpdateHelper() {}
}