using UnityEngine;

public class PowerPellet : AbstractEatable
{
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;

    protected override void OnPlayerDetection()
    {
        powerPelletChannel.Invoke();
    }

    protected override void OnNextLevel()
    {
        gameObject.SetActive(true);
    }
}