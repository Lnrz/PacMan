using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private PointsChannelSO pointsChannel;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private GhostEatenChannelSO ghostEatenChannel;
    [SerializeField] private int[] eatGhostPoints = { 200, 400, 800, 1600};
    private int points = 0;
    private int eatGhostProgress = 0;

    private void Awake()
    {
        pointsChannel.AddListener(IncreasePoints);
        powerPelletChannel.AddListener(ResetEatGhostProgress);
        ghostEatenChannel.AddListener(OnGhostEaten);
    }

    private void IncreasePoints(int points)
    {
        this.points += points;
    }

    private void ResetEatGhostProgress()
    {
        eatGhostProgress = 0;
    }

    private void OnGhostEaten()
    {
        IncreasePoints(eatGhostPoints[eatGhostProgress]);
        eatGhostProgress++;
    }
}