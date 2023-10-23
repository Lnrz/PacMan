using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    private bool isGameStarted = false;

    private void Awake()
    {
        gameRestartChannel.AddListener(OnGameRestart);
    }

    private void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.T))
        {
            isGameStarted = true;
            gameStartChannel.Invoke();
        }
    }

    private void OnGameRestart()
    {
        isGameStarted = false;
    }
}