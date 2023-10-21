using System.Collections;
using UnityEngine;

public class PlayerMovement : AbstractMovingEntity
{
    [SerializeField] private float normalSpeedMod = 0.8f;
    [SerializeField] private float poweredSpeedMod = 0.9f;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    private float currentSpeedMod;
    private bool isEating = false;

    private void Awake()
    {
        powerPelletChannel.AddListener(ChangeSpeedModToPowered);
        powerUpEndChannel.AddListener(ChangeSpeedModToNormal);
        ChangeSpeedModToNormal();
        if (TryGetComponent<PlayerInputEventInvoker>(out PlayerInputEventInvoker playerInputEventInvoker))
        {
            playerInputEventInvoker.OnPlayerInputEvent(ChangeDirection);
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

    public override sealed void IntersectionStopEnter(Vector3 interPos) {} 

    protected override void UpdateHelper() {}
}