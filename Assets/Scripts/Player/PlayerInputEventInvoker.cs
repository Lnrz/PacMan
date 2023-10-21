using UnityEngine.Events;

public interface PlayerInputEventInvoker
{
    public void OnPlayerInputEvent(UnityAction<int> listener);
}