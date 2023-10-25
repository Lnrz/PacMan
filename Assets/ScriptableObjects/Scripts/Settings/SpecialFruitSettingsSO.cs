using UnityEngine;

[CreateAssetMenu(fileName = "SpecialFruitSettingsSO", menuName = "ScriptableObjects/Settings/SpecialFruitSettings", order = 1)]
public class SpecialFruitSettingsSO : ScriptableObject
{
    [SerializeField] private GameObject specialFruitPrefab;

    public GameObject GetSpecialFruitPrefab()
    {
        return specialFruitPrefab;
    }
}