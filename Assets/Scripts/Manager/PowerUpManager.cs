using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private PowerUpDurationSettingsChannelSO powerUpDurationSettingsChannel;
    [SerializeField] private BlinkingChannelSO blinkingChannel;
    [SerializeField] private float blinkingFraction;
    private float powerUpDuration;
    private IEnumerator timerCoroutine;

    private void Awake()
    {
        powerPelletChannel.AddListener(StartTimer);
        powerUpDurationSettingsChannel.AddListener(OnPowerUpDurationSettingsChange);
    }

    private void StartTimer()
    {
        if (timerCoroutine is not null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = Timer();
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(powerUpDuration * (1.0f - blinkingFraction));
        blinkingChannel.Invoke();
        yield return new WaitForSeconds(powerUpDuration * blinkingFraction);
        powerUpEndChannel.Invoke();
    }

    private void OnPowerUpDurationSettingsChange(PowerUpDurationSettings powerUpDurationSettings)
    {
        powerUpDuration = powerUpDurationSettings.GetDuration();
    }
}