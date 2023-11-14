using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private PointsChannelSO pointsChannel;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private GhostEatenChannelSO ghostEatenChannel;
    [SerializeField] private PointsTextChannelSO pointsTextChannel;
    [SerializeField] private ScoreUpdateChannelSO scoreUpdateChannel;
    [SerializeField] private HighscoreUpdateChannelSO highscoreUpdateChannel;
    [SerializeField] private int[] eatGhostPoints = { 200, 400, 800, 1600};
    private int currentPoints = 0;
    private int highscore = 0;
    private int eatGhostProgress = 0;

    private void Awake()
    {
        GetInitialHighscore();
        pointsChannel.AddListener(IncreasePoints);
        powerPelletChannel.AddListener(OnPowerPelletEaten);
        ghostEatenChannel.AddListener(OnGhostEaten);
    }

    private void GetInitialHighscore()
    {
        highscore = Utility.GetHighscore();
    }

    private void IncreasePoints(int points)
    {
        currentPoints += points;
        scoreUpdateChannel.Invoke(currentPoints);
        if (highscore < currentPoints)
        {
            highscore = currentPoints;
            Utility.SetHighscore(highscore);
            highscoreUpdateChannel.Invoke(highscore);
        }
    }

    private void OnPowerPelletEaten()
    {
        eatGhostProgress = 0;
    }

    private void OnGhostEaten(Vector3 pos)
    {
        IncreasePoints(eatGhostPoints[eatGhostProgress]);
        pointsTextChannel.Invoke(eatGhostPoints[eatGhostProgress], pos);
        eatGhostProgress++;
    }
}