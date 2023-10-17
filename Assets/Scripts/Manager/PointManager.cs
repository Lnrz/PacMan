using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private PointsChannelSO pointsChannel;
    private int points = 0;

    private void Awake()
    {
        pointsChannel.AddListener(IncreasePoints);
    }

    private void IncreasePoints(int points)
    {
        this.points += points;
    }
}