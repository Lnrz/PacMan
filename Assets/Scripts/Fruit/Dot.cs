using UnityEngine;

public class Dot : AbstractEatable
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private DotPowerPelletEatenChannelSO dotPowerPelletEatenChannel;

    protected override void OnPlayerDetection()
    {
        dotChannel.Invoke();
        dotPowerPelletEatenChannel.Invoke();
    }

    protected override void OnNextLevel()
    {
        gameObject.SetActive(true);
    }
}