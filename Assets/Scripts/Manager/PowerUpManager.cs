using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private PowerUpDurationSettingsChannelSO powerUpDurationSettingsChannel;
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
        yield return new WaitForSeconds(powerUpDuration);
        powerUpEndChannel.Invoke();
    }

    private void OnPowerUpDurationSettingsChange(PowerUpDurationSettings powerUpDurationSettings)
    {
        powerUpDuration = powerUpDurationSettings.GetDuration();
    }
}