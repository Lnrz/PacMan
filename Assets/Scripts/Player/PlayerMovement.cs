using System.Collections;
using UnityEngine;

public class PlayerMovement : AbstractMovingEntity
{
    [SerializeField] private bool[] startDirections;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private SpeedSettingsChannelSO speedSettingsChannel;
    private float normalSpeedMod;
    private float poweredSpeedMod;
    private float currentSpeedMod;
    private bool isEating = false;

    protected override void AwakeHelper()
    {
        transform.position = startPos;
        SetLegalDir(startDirections);
        powerPelletChannel.AddListener(ChangeSpeedModToPowered);
        powerUpEndChannel.AddListener(ChangeSpeedModToNormal);
        speedSettingsChannel.AddListener(OnSpeedSettingsChange);
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

    private void OnPlayerInput(int inputDirectionIndex)
    {
        ChangeDirection(inputDirectionIndex);
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

    protected override void OnGameRestart()
    {
        transform.position = startPos;
        isEating = false;
        ChangeSpeedModToNormal();
        SetLegalDir(startDirections);
    }

    public override sealed void IntersectionStopEnter(Vector3 interPos) {} 

    protected override void UpdateHelper() {}
}