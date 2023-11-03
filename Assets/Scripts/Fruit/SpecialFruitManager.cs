using UnityEngine;

public class SpecialFruitManager : MonoBehaviour
{
    [SerializeField] private int[] waitDots;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private SpecialFruitSettingsChannelSO specialFruitChannel;
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    private GameObject specialFruitPrefab;
    private int dotsEaten = 0;
    private int progress = 0;

    private void Awake()
    {
        dotChannel.AddListener(OnDotEaten);
        specialFruitChannel.AddListener(OnSpecialFruitSettingsChange);
        nextLevelChannel.AddListener(OnNextLevel);
    }

    private void OnDotEaten()
    {
        if (progress < waitDots.Length)
        {
            dotsEaten++;
            if (dotsEaten == waitDots[progress])
            {
                Instantiate(specialFruitPrefab, spawnPos, Quaternion.identity);
                progress++;
            }
        }
    }

    private void OnSpecialFruitSettingsChange(SpecialFruitSettingsSO specialFruitSettings)
    {
        specialFruitPrefab = specialFruitSettings.GetSpecialFruitPrefab();
    }

    private void OnNextLevel()
    {
        dotsEaten = 0;
        progress = 0;
    }
}