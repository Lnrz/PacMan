using UnityEngine;

public class PowerPellet : AbstractEatable
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private DotPowerPelletEatenChannelSO dotPowerPelletEatenChannel;

    protected override void OnPlayerDetection()
    {
        powerPelletChannel.Invoke();
        dotPowerPelletEatenChannel.Invoke();
    }

    protected override void OnNextLevel()
    {
        gameObject.SetActive(true);
    }
}