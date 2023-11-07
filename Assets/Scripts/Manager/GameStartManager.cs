using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private float startWaitTime = 5.0f;

    private void Awake()
    {
        gameRestartChannel.AddListener(OnGameRestart);
        StartCoroutine(GameStartWait());
    }

    private void OnGameRestart()
    {
        StartCoroutine(GameStartWait());
    }

    private IEnumerator GameStartWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        gameStartChannel.Invoke();
    }
}