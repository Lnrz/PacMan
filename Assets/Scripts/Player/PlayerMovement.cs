using System.Collections;
using UnityEngine;

public class PlayerMovement : AbstractMovingEntity
{
    [SerializeField] private bool[] startDirections;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private SpeedSettingsChannelSO speedSettingsChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    private float normalSpeedMod;
    private float poweredSpeedMod;
    private float currentSpeedMod;
    private bool isEating = false;
    private bool isGameStarted = false;

    protected override void AwakeHelper()
    {
        transform.position = startPos;
        SetStartingTurnableDirections();
        powerPelletChannel.AddListener(ChangeSpeedModToPowered);
        powerUpEndChannel.AddListener(ChangeSpeedModToNormal);
        speedSettingsChannel.AddListener(OnSpeedSettingsChange);
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
        if (TryGetComponent<PlayerInputEventInvoker>(out PlayerInputEventInvoker playerInputEventInvoker))
        {
            playerInputEventInvoker.OnPlayerInputEvent(OnPlayerInput);
        }
    }

    private void OnSpeedSettingsChange(SpeedSettingsSO speedSettings)
    {
        normalSpeedMod = speedSettings.GetPlayerNormalSpeedMod();
        poweredSpeedMod = speedSettings.GetPlayerPoweredSpeedMod();
        ChangeSpeedModToNormal();
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
        SetStartingTurnableDirections();
    }

    private void SetStartingTurnableDirections()
    {
        for (int i = 0; i < 4; i++)
        {
            LockDirection(i, !startDirections[i]);
        }
    }

    public override sealed void IntersectionStopEnter(Vector3 interPos) {} 

    protected override void UpdateHelper() {}
}