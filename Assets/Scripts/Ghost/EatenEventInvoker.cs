using UnityEngine.Events;

public interface EatenEventInvoker
{
    public void OnEaten(UnityAction listener);
}