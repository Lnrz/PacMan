using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    private bool isGameStarted = false;

    private void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.T))
        {
            isGameStarted = true;
            gameStartChannel.Invoke();
        }
    }
}