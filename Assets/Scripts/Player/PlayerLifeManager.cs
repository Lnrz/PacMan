using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int livesLeft = 4;
    [SerializeField] private LivesLeftUpdateChannelSO livesLeftUpdateChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;

    private void Start()
    {
        livesLeftUpdateChannel.Invoke(livesLeft);
    }

    public void DecreaseLivesLeft()
    {
        livesLeft--;
        if (livesLeft >= 0)
        {
            livesLeftUpdateChannel.Invoke(livesLeft);
            gameRestartChannel.Invoke();
        }
        else
        {
            gameEndChannel.Invoke();
            Utility.LoadScene("MenuScene", false);
            Utility.UnloadScene("GameboardScene");
        }
    }
}