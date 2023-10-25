using UnityEngine;

public class SpecialFruitManager : MonoBehaviour
{
    [SerializeField] private int[] waitDots = { 70, 100};
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private SpecialFruitSettingsChannelSO specialFruitChannel;
    private GameObject specialFruitPrefab;
    private int progress = 0;

    private void Awake()
    {
        dotChannel.AddListener(DecreaseCounter);
        specialFruitChannel.AddListener(OnSpecialFruitSettingsChange);
    }

    private void DecreaseCounter()
    {
        if (progress < waitDots.Length)
        {
            waitDots[progress]--;
            if (waitDots[progress] == 0)
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
}