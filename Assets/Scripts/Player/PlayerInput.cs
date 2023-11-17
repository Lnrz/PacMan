using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour, PlayerInputEventInvoker
{
    private int inputDirectionIndex;
    private UnityEvent<int> listeners = new UnityEvent<int>();

    private void Update()
    {
        inputDirectionIndex = GetDirectionInput();
        if (inputDirectionIndex != -1)
        {
            listeners.Invoke(inputDirectionIndex);
        }
    }

    private int GetDirectionInput()
    {
        int directionIndex = -1;

        if (UpInput())
        {
            directionIndex = 0;
        }
        else if (RightInput())
        {
            directionIndex = 1;
        }
        else if (DownInput())
        {
            directionIndex = 2;
        }
        else if (LeftInput())
        {
            directionIndex = 3;
        }
        return directionIndex;
    }

    private bool UpInput()
    {
        return Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.Keypad8);
    }

    private bool RightInput()
    {
        return Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.Keypad6);
    }

    private bool DownInput()
    {
        return Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.Keypad5);
    }

    private bool LeftInput()
    {
        return Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.Keypad4);
    }

    public void OnPlayerInputEvent(UnityAction<int> listener)
    {
        listeners.AddListener(listener);
    }
}