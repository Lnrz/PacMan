using UnityEngine;

public class TempGameEndBehav : MonoBehaviour
{
    [SerializeField] private GameEndChannelSO gameEndChannel;

    private void Awake()
    {
        gameEndChannel.AddListener(() => gameObject.SetActive(false));
    }
}