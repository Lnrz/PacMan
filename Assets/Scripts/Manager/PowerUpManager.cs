using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private float powerUpDuration = 20.0f;
    private IEnumerator timerCoroutine;

    private void Awake()
    {
        powerPelletChannel.AddListener(StartTimer);
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
}