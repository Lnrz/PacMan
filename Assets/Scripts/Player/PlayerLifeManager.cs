using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int livesLeft = 2;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;

    public void DecreaseLivesLeft()
    {
        livesLeft--;
        if (livesLeft >= 0)
        {
            gameRestartChannel.Invoke();
        }
        else
        {
            gameEndChannel.Invoke();
        }
    }
}