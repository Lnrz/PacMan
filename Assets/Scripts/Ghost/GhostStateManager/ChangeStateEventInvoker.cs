using UnityEngine.Events;

public interface ChangeStateEventInvoker
{
    public void OnChangeState(UnityAction<GhostStateAbstractFactory> listener);
}