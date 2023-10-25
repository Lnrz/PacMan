using UnityEngine;

public class Dot : AbstractEatable
{
    [SerializeField] private DotChannelSO dotChannel;

    protected override void OnPlayerDetection()
    {
        dotChannel.Invoke();
    }

    protected override void OnNextLevel()
    {
        gameObject.SetActive(true);
    }
}