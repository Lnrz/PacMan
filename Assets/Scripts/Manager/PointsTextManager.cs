using TMPro;
using UnityEngine;

public class PointsTextManager : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private PointsTextChannelSO pointsTextChannel;
    [SerializeField] private float textDuration;

    private void Awake()
    {
        pointsTextChannel.AddListener(CreatePointsText);
    }

    private void CreatePointsText(int points, Vector3 pos)
    {
        GameObject go;
        TMP_Text text;

        go = Instantiate(textPrefab, pos, Quaternion.identity);
        text = go.GetComponent<TMP_Text>();
        text.text = points.ToString();
        Destroy(go, textDuration);
    }
}