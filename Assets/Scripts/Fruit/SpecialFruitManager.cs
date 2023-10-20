using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFruitManager : MonoBehaviour
{
    [SerializeField] private int[] waitDots = { 70, 170};
    [SerializeField] private GameObject specialFruitPrefab;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private DotChannelSO dotChannel;
    private int progress = 0;

    private void Awake()
    {
        dotChannel.AddListener(DecreaseCounter);
    }

    private void DecreaseCounter()
    {
        for (int i = progress; i < waitDots.Length; i++)
        {
            waitDots[i]--;
        }
        if (waitDots[progress] == 0)
        {
            Instantiate(specialFruitPrefab, spawnPos, Quaternion.identity);
            progress++;
            if (progress == waitDots.Length)
            {
                dotChannel.RemoveListener(DecreaseCounter);
            }
        }
    }
}