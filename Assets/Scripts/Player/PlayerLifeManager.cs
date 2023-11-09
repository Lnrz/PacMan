using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private LivesLeftUpdateChannelSO livesLeftUpdateChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private PointsChannelSO pointsChannel;
    [SerializeField] private PlayerEatenChannelSO playerEatenChannel;
    [SerializeField] private int livesLeft = 4;
    [SerializeField] private int pointsForBonusLife = 10000;
    private bool canBeEaten = false;

    private void Awake()
    {
        gameStartChannel.AddListener(OnGameStart);
        pointsChannel.AddListener(OnPointsUpdate);
    }

    private void Start()
    {
        livesLeftUpdateChannel.Invoke(livesLeft);
    }

    private void OnGameStart()
    {
        canBeEaten = true;
    }

    private void OnPointsUpdate(int points)
    {
        if (points >= pointsForBonusLife)
        {
            UpdateLivesLeft(1);
            pointsChannel.RemoveListener(OnPointsUpdate);
        }
    }

    public void DecreaseLivesLeft()
    {
        if (canBeEaten)
        {
            canBeEaten = false;
            UpdateLivesLeft(-1);
            playerEatenChannel.Invoke();
        }
    }

    private void UpdateLivesLeft(int delta)
    {
        livesLeft += delta;
        livesLeftUpdateChannel.Invoke(livesLeft);
    }
}