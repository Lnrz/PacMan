using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private FruitChannelSO fruitChannel;
    private int points = 0;

    private void Awake()
    {
        fruitChannel.AddListener(points => IncreasePoints(points));
    }

    private void IncreasePoints(int points)
    {
        this.points += points;
    }
}