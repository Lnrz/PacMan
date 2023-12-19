using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip gameStartAudioClip;
    [SerializeField] private float gameStartWait;
    private bool firstStart = true;

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
        if (firstStart)
        {
            audioSrc.PlayOneShot(gameStartAudioClip);
            firstStart = false;
            yield return new WaitForSeconds(gameStartAudioClip.length);
        }
        else
        {
            yield return new WaitForSeconds(gameStartWait);
        }
        gameStartChannel.Invoke();
    }
}